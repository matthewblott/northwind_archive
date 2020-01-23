using northwind.services;

namespace northwind.web.ui.models
{
  public class CategoryQueryParameters : QueryParameters, IQueryParameters
  {
    public long Id { get; set; }
    public string CategoryName { get; set; }
    public string Description { get; set; }
  }
}