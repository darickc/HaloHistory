using System;
using HaloHistory.Business;
using HaloHistory.Business.Entities;
using HaloHistory.Business.Repositories.Metadata;
using HaloHistory.Business.Repositories.Profile;
using HaloHistory.Business.Repositories.Stats;
using HaloHistory.Business.Utilities;
using HaloHistory.Web.Models;
using HaloHistory.Web.Services;
using HaloSharp;
using HaloSharp.Model;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HaloHistory.Web
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
            

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var connection = Configuration["Data:DefaultConnection:ConnectionString"];
            // Add framework services.
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<HaloHistoryContext>(options =>
                {
                    options.UseSqlServer(connection);
                });

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            services.Configure<Settings>(Configuration.GetSection("AppSettings"));

            var settings = new Settings
            {
                CacheResults = bool.Parse(Configuration["AppSettings:CacheResults"]),
                ApiKey = Configuration["AppSettings:ApiKey"],
                RequestCount = int.Parse(Configuration["AppSettings:RequestCount"]),
                RequestTime = int.Parse(Configuration["AppSettings:RequestTime"])
            };

            services.AddInstance<IProduct>(new Product
            {
                SubscriptionKey = settings.ApiKey,
                RateLimit = new RateLimit
                {
                    RequestCount = settings.RequestCount,
                    TimeSpan = new TimeSpan(0, 0, 0, settings.RequestTime),
                    Timeout = new TimeSpan(0, 0, 0, 10)
                }
            });

            services.AddInstance<ISettings>(settings);

            services.AddInstance<ICacheSettings>(new CacheSettings
            {
                MetadataCacheDuration = new TimeSpan(0, 0, 10, 0),
                ProfileCacheDuration = new TimeSpan(0, 0, 10, 0),
                StatsCacheDuration = null //Don't cache 'Stats' endpoints.
            });

            //services.AddInstance<IContextInitializer>(new ContextInitializer {FileLocation = "cartographer.db"});

            services.AddTransient<IObjectCache, HaloObjectCache>();
            services.AddTransient<IHaloSharpTimer, HaloSharpTimer>();
            services.AddSingleton<IHaloSession, HaloSession>();
            services.AddSingleton<IDataCache, DataCache>();

            services.AddScoped<ICartographerContext, HaloHistoryContext>();

            services.AddScoped<IMetadataRepository, MetadataRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IStatsRepository, StatsRepository>();

            services.AddScoped<IProfileBusiness, ProfileBusiness>();
            services.AddScoped<IStatsBusiness, StatsBusiness>();
            AutoMapperConfiguration.Configure();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
            }
            else
            {
                //app.UseExceptionHandler("/Home/Error");

                // For more details on creating database during deployment see http://go.microsoft.com/fwlink/?LinkID=615859

            }
            try
            {
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                    .CreateScope())
                {
                    serviceScope.ServiceProvider.GetService<HaloHistoryContext>()
                        .Database.Migrate();
                }
            }
            catch (Exception ex)
            {

            }

            app.UseIISPlatformHandler();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "CatchAll",
                    "{*url}",
                    new { controller = "Home", action = "Index" });
            });
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
