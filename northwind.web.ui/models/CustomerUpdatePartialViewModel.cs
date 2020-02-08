using northwind.common.mapping;
using northwind.services.commands;

namespace northwind.web.ui.models
{
  public class CustomerUpdatePartialViewModel : IMapTo<CustomerUpdatePartial>
  {
    public string Id { get; set; }
    public string CompanyName { get; set; }
    public string Region { get; set; }
    public string ReturnUrl { get; set; }
    
  }
  
}