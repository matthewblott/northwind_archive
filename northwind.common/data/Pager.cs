namespace northwind.common.data
{
  public class Pager
  {
    private const int DefaultPageSize = 10;
    public int PageNumber { get; }
    public int PageSize { get; }

    public long TotalItems { get; }
    
    public Pager(int pageNumber)
    {
      PageNumber = pageNumber == default ? 1 : pageNumber;
      PageSize = DefaultPageSize;
    }

    public Pager(int pageNumber, int totalItems)
    {
      PageNumber = pageNumber == default ? 1 : pageNumber;
      PageSize = DefaultPageSize;
      TotalItems = totalItems;
    }
    
  }
  
}