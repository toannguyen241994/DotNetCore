using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore.Objects
{
    public static class PagedListExtensions
    {
        public static PagedList<T> List<T>(this IQueryable<T> queryable, PagedListParameters parameters)
        {
            return new PagedList<T>(queryable, parameters);
        }

        public static Task<PagedList<T>> ListAsync<T>(this IQueryable<T> queryable, PagedListParameters parameters)
        {
            return Task.FromResult(new PagedList<T>(queryable, parameters));
        }
    }
}
