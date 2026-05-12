namespace ProCleanArchitecture.Application.Common.Models;

public class PagedResult<T>
{
    public PagedResult(IReadOnlyList<T> items, int totalCount, int pageNumber, int pageSize, string? searchTerm)
    {
        Items = items;
        TotalCount = totalCount;
        PageNumber = pageNumber;
        PageSize = pageSize;
        SearchTerm = searchTerm;
    }

    public IReadOnlyList<T> Items { get; }
    public int TotalCount { get; }
    public int PageNumber { get; }
    public int PageSize { get; }
    public string? SearchTerm { get; }
    public int TotalPages => PageSize <= 0 ? 0 : (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
}
