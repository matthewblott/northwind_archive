namespace northwind.services.models.customers
{
  using common.mapping;
  using domain.models;
  public class CustomerServiceModel : IMapFrom<Customer>
  {
    public string Id { get; set; }
    public string CompanyName { get; set; }
    public string Region { get; set; }
  }
}