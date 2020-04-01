using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace DotNetCore.EntityFrameworkCore
{
    public static class Extensions
    {
        public static void AddConfigurationsFromAssembly(this ModelBuilder modelBuilder)
        {
            static bool Expression(Type type) => type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>);

            var types = Assembly.GetCallingAssembly().GetTypes().Where(type => type.GetInterfaces().Any(Expression)).ToList();

            var configurations = types.Select(Activator.CreateInstance);

            foreach (var configuration in configurations)
            {
                modelBuilder.ApplyConfiguration((dynamic)configuration);
            }
        }

        public static void AddContextMemory<T>(this IServiceCollection services) where T : DbContext
        {
            services.AddDbContextPool<T>(options => options.UseInMemoryDatabase(typeof(T).Name));

            services.BuildServiceProvider().GetRequiredService<T>().Database.EnsureCreated();
        }

        public static void AddContextMigrate<T>(this IServiceCollection services, Action<DbContextOptionsBuilder> options) where T : DbContext
        {
            services.AddDbContextPool<T>(options);

            services.BuildServiceProvider().GetRequiredService<T>().Database.Migrate();
        }

        public static DbSet<T> CommandSet<T>(this DbContext context) where T : class
        {
            return context.DetectChangesLazyLoading(true).Set<T>();
        }

        public static DbContext DetectChangesLazyLoading(this DbContext context, bool enabled)
        {
            context.ChangeTracker.AutoDetectChangesEnabled = enabled;

            context.ChangeTracker.LazyLoadingEnabled = enabled;

            context.ChangeTracker.QueryTrackingBehavior = enabled ? QueryTrackingBehavior.TrackAll : QueryTrackingBehavior.NoTracking;

            return context;
        }

        public static IQueryable<T> QuerySet<T>(this DbContext context) where T : class
        {
            return context.DetectChangesLazyLoading(false).Set<T>().AsNoTracking();
        }
    }
}
