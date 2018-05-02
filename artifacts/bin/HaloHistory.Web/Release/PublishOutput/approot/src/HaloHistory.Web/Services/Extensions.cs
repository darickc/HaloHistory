using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace HaloHistory.Web.Services
{
    public static class Extensions
    {
        public static async Task<TEntity> Find<TEntity>(this DbSet<TEntity> set, params object[] keyValues) where TEntity : class
        {
            // TODO: Build the real LINQ Expression
            // set.Where(x => x.Id == keyValues[0]);
            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var query = set.Where((Expression<Func<TEntity, bool>>)
                Expression.Lambda(
                    Expression.Equal(
                        Expression.Property(parameter, "ItemId"),
                        Expression.Constant(keyValues[0])),
                    parameter));

            // Look in the database
            return await query.FirstOrDefaultAsync();
        }
    }
}
