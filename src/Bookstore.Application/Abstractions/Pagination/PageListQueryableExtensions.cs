using Microsoft.EntityFrameworkCore;

namespace Bookstore.Application.Abstractions.Pagination;

public static class PageListQueryableExtensions
{
    public static async Task<PaginationResult<T>> ToPagedListAsync<T>(
        this IQueryable<T> source,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var count = await source.CountAsync(cancellationToken);

        if (count <= 0)
        {
            return new PaginationResult<T>(new List<T>(), pageNumber, pageSize, count);
        }

        var items = await source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PaginationResult<T>(items, pageNumber, pageSize, count);
    }
}
