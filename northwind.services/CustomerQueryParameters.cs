namespace northwind.services
{
  public class CustomerQueryParameters : QueryParameters, IQueryParameters
  {
    public string Id { get; set; }
    public string CompanyName { get; set; }
  }
}