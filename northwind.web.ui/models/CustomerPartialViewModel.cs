using northwind.common.mapping;
using northwind.domain.models;

namespace northwind.web.ui.models
{
  public class CustomerPartialViewModel : IMapFrom<Customer>, IMapTo<CustomerModalViewModel>
  {
    public string Id { get; set; }
    public string CompanyName { get; set; }
    public string Region { get; set; }

  }
  
}