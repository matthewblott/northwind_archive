using cloudscribe.Pagination.Models;
using northwind.domain.models;
using northwind.web.ui.models;

namespace northwind.web.ui.setup
{
  public class MappingProfile : AutoMapper.Profile
  {
    public MappingProfile()
    {
      CreateMap<Category, CategoryViewModel>().ReverseMap();
      CreateMap<PagedResult<Category>, PagedResult<CategoryViewModel>>();

      CreateMap<Customer, CustomerViewModel>().ReverseMap();
      CreateMap<Customer, PartialCustomerViewModel>().ReverseMap();
      CreateMap<PagedResult<Customer>, PagedResult<PartialCustomerViewModel>>();
      
      CreateMap<Product, ProductViewModel>().ReverseMap();
      CreateMap<PagedResult<Product>, PagedResult<ProductViewModel>>();

    }
    
  }
  
}