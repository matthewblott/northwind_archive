namespace northwind.services.types
{
  public class Page
  {
    private const int DefaultPageSize = 10;
    public long PageNumber { get; }
    public int PageSize { get; }

    public Page(long pageNumber)
    {
      PageNumber = pageNumber == default ? 1 : pageNumber;
      PageSize = DefaultPageSize;
    }

  }
  
}