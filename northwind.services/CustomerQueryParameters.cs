namespace northwind.services
{

  public class RegionQueryParameters : QueryParameters, IQueryParameters
  {
    public int Id { get; set; }
    public string RegionDescription { get; set; }
  }
  
  public class CustomerQueryParameters : QueryParameters, IQueryParameters
  {
    public string Id { get; set; }
    public string CompanyName { get; set; }
  }
}