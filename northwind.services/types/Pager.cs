namespace northwind.services.types
{
  public class Pager
  {
    private const int DefaultPageSize = 10;
    public long PageNumber { get; }
    public int PageSize { get; }

    public long TotalItems { get; }
    
    public Pager(long pageNumber)
    {
      PageNumber = pageNumber == default ? 1 : pageNumber;
      PageSize = DefaultPageSize;
    }

    public Pager(long pageNumber, long totalItems)
    {
      PageNumber = pageNumber == default ? 1 : pageNumber;
      PageSize = DefaultPageSize;
      TotalItems = totalItems;
    }
  }
  
}