namespace Repository.Extensions
{
    public static class PagedExtension
    {
        public static IEnumerable<T> Paged<T>(this IEnumerable<T> query, int size, int page)
        {
            return query.Skip((page - 1) * size).Take(size).ToList();
        }
    }
}