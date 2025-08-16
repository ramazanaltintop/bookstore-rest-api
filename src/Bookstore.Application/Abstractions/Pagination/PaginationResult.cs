namespace Bookstore.Application.Abstractions.Pagination;

public class PaginationResult<T>
{
    public PaginationResult(IList<T> datas, int pageNumber, int pageSize, int totalCount)
    {
        Datas = datas;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPage = (int)Math.Ceiling(totalCount / (double)pageSize);
        IsFirstPage = PageNumber == 1;
        IsLastPage = PageNumber == TotalPage;
    }
    public IList<T> Datas { get; }
    public int PageNumber { get; }
    public int PageSize { get; }
    public int TotalPage { get; }
    public bool IsFirstPage { get; }
    public bool IsLastPage { get; }
}
