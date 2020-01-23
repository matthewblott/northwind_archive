namespace northwind.services
{
  public class ProductQueryParameters : QueryParameters, IQueryParameters
  {
    public long Id { get; set; }
    public string ProductName { get; set; }
  }
}