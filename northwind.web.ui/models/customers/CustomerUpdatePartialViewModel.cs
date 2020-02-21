namespace northwind.web.ui.models.customers
{
  using common.mapping;
  using services.commands;
  
  public class CustomerUpdatePartialViewModel : IMapTo<CustomerUpdatePartial>
  {
    public string Id { get; set; }
    public string CompanyName { get; set; }
    public string Region { get; set; }
    public string ReturnUrl { get; set; }
    
  }
  
}