namespace northwind.services
{
  public class CustomerQueryParameters : IQueryParameters
  {
    public string OrderBy { get; set; }
    public bool IsDescending { get; set; }
    public string Id { get; set; }
  }
}