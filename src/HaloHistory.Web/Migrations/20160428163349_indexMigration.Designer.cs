using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using HaloHistory.Web.Models;

namespace HaloHistory.Web.Migrations
{
    [DbContext(typeof(HaloHistoryContext))]
    [Migration("20160428163349_indexMigration")]
    partial class indexMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HaloHistory.Business.Entities.Metadata.CampaignMissionData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Data");

                    b.Property<string>("ItemId")
                        .HasAnnotation("MaxLength", 36);

                    b.HasKey("Id");
                });

            modelBuilder.Entity("HaloHistory.Business.Entities.Metadata.CommendationData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Data");

                    b.Property<string>("ItemId")
                        .HasAnnotation("MaxLength", 36);

                    b.HasKey("Id");
                });

            modelBuilder.Entity("HaloHistory.Business.Entities.Metadata.CompetitiveSkillRankDesignationData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Data");

                    b.Property<int>("ItemId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("HaloHistory.Business.Entities.Metadata.EnemyData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Data");

                    b.Property<long>("ItemId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("HaloHistory.Business.Entities.Metadata.FlexibleStatData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Data");

                    b.Property<string>("ItemId")
                        .HasAnnotation("MaxLength", 36);

                    b.HasKey("Id");
                });

            modelBuilder.Entity("HaloHistory.Business.Entities.Metadata.GameBaseVariantData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Data");

                    b.Property<string>("ItemId")
                        .HasAnnotation("MaxLength", 36);

                    b.HasKey("Id");
                });

            modelBuilder.Entity("HaloHistory.Business.Entities.Metadata.GameVariantData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Data");

                    b.Property<string>("ItemId")
                        .HasAnnotation("MaxLength", 36);

                    b.HasKey("Id");
                });

            modelBuilder.Entity("HaloHistory.Business.Entities.Metadata.ImpulseData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Data");

                    b.Property<long>("ItemId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("HaloHistory.Business.Entities.Metadata.MapData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Data");

                    b.Property<string>("ItemId")
                        .HasAnnotation("MaxLength", 36);

                    b.HasKey("Id");
                });

            modelBuilder.Entity("HaloHistory.Business.Entities.Metadata.MapVariantData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Data");

                    b.Property<string>("ItemId")
                        .HasAnnotation("MaxLength", 36);

                    b.HasKey("Id");
                });

            modelBuilder.Entity("HaloHistory.Business.Entities.Metadata.MedalData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Data");

                    b.Property<long>("ItemId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("HaloHistory.Business.Entities.Metadata.PlaylistData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Data");

                    b.Property<string>("ItemId")
                        .HasAnnotation("MaxLength", 36);

                    b.HasKey("Id");
                });

            modelBuilder.Entity("HaloHistory.Business.Entities.Metadata.SeasonData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Data");

                    b.Property<string>("ItemId")
                        .HasAnnotation("MaxLength", 36);

                    b.HasKey("Id");
                });

            modelBuilder.Entity("HaloHistory.Business.Entities.Metadata.SkullData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Data");

                    b.Property<int>("ItemId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("HaloHistory.Business.Entities.Metadata.SpartanRankData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Data");

                    b.Property<int>("ItemId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("HaloHistory.Business.Entities.Metadata.TeamColorData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Data");

                    b.Property<int>("ItemId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("HaloHistory.Business.Entities.Metadata.VehicleData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Data");

                    b.Property<long>("ItemId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("HaloHistory.Business.Entities.Metadata.WeaponData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Data");

                    b.Property<long>("ItemId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("HaloHistory.Business.Entities.Profile.EmblemImageData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Data");

                    b.Property<string>("ItemId")
                        .HasAnnotation("MaxLength", 36);

                    b.Property<DateTime>("Timestamp");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");
                });

            modelBuilder.Entity("HaloHistory.Business.Entities.Profile.SpartanImageData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Data");

                    b.Property<string>("ItemId")
                        .HasAnnotation("MaxLength", 36);

                    b.Property<DateTime>("Timestamp");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");
                });

            modelBuilder.Entity("HaloHistory.Business.Entities.Stats.MatchResultData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Data");

                    b.Property<string>("ItemId")
                        .HasAnnotation("MaxLength", 36);

                    b.HasKey("Id");

                    b.HasIndex("ItemId");
                });
        }
    }
}
