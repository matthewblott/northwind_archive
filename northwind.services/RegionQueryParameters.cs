namespace northwind.services
{
  public class RegionQueryParameters : QueryParameters, IQueryParameters
  {
    public int Id { get; set; }
    public string RegionDescription { get; set; }
  }
}