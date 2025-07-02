using System.Linq.Expressions;

namespace API.Infrastructure.Extensions
{
    public static class QueryableExtension
    {
        public static IQueryable<T> WhereIfNotNull<T, TProperty>(
        this IQueryable<T> query,
        TProperty? value,
        Expression<Func<T, bool>> predicate
    )
        {
            if (value == null)
                return query;

            return query.Where(predicate);
        }
        public static IQueryable<T> PageBy<T>(this IQueryable<T> source, int? pageNumber, int? pageSize)
        {
            if (pageNumber == null || pageSize == null || pageNumber <= 0 || pageSize <= 0)
            {
                return source; // 没有分页信息就返回原始数据
            }

            int skip = ((int)pageNumber - 1) * (int)pageSize;
            return source.Skip(skip).Take((int)pageSize);
        }

    }
}
