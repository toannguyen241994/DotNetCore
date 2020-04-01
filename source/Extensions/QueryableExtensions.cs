using System;
using System.Linq;
using System.Linq.Expressions;

namespace DotNetCore.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Order<T>(this IQueryable<T> queryable, string property, bool ascending)
        {
            if (queryable == null || string.IsNullOrEmpty(property))
            {
                return queryable;
            }

            var properties = property.Split('.');

            var parameters = Expression.Parameter(typeof(T));

            var body = properties.Aggregate<string, Expression>(parameters, Expression.Property);

            body = Expression.Convert(body, typeof(object));

            var expression = Expression.Lambda<Func<T, object>>(body, parameters).Compile();

            return ascending ? queryable.OrderBy(expression).AsQueryable() : queryable.OrderByDescending(expression).AsQueryable();
        }

        public static IQueryable<T> Page<T>(this IQueryable<T> queryable, int index, int size)
        {
            if (queryable == null || !queryable.Any() || index == 0 || size == 0)
            {
                return queryable;
            }

            return queryable.Skip((index - 1) * size).Take(size);
        }
    }
}
