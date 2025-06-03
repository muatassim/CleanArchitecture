using CleanArchitecture.Core.Abstractions;
using CleanArchitecture.Infrastructure.MicrosoftSql.Repositories;
using Microsoft.EntityFrameworkCore;
namespace CleanArchitecture.Infrastructure.MicrosoftSql.Extensions
{
    public static class DbContextExtensions
    {
        public static void DetachLocal<T, TEntityId>(this ApplicationDbContext context, T t, TEntityId id)
            where T : Entity<TEntityId>
        {
            var local = context.Set<T>()
                .Local
                .FirstOrDefault(entry => entry.Id != null && entry.Id.Equals(id)); // Added null check for entry.Id
            if (local != null)
            {
                context.Entry(local).State = EntityState.Detached;
            }
            context.Entry(t).State = EntityState.Modified;
        }

        public static void DetachLocalEntity<T, TEntityId>(this ApplicationDbContext context, T t, params object[] pkey)
            where T : Entity<TEntityId>
        {
            var local = context.Set<T>().Find(pkey);
            if (local != null)
            {
                context.Entry(local).State = EntityState.Detached;
            }
            context.Entry(t).State = EntityState.Modified;
        }
    }
}
