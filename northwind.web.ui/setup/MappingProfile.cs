using cloudscribe.Pagination.Models;
using northwind.domain.models;
using northwind.web.ui.models;

namespace northwind.web.ui.setup
{
  public class MappingProfile : AutoMapper.Profile
  {
    public MappingProfile()
    {
      CreateMap<Customer, CustomerViewModel>().ReverseMap();
      CreateMap<PagedResult<Customer>, PagedResult<CustomerViewModel>>();

    }
    
  }
  
}