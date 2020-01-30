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
      CreateMap<Customer, CustomerPartialViewModel>().ReverseMap();
      CreateMap<CustomerPartialViewModel, CustomerModalViewModel>();
      CreateMap<PagedResult<Customer>, PagedResult<CustomerPartialViewModel>>();
      CreateMap<Product, ProductViewModel>().ReverseMap();
      CreateMap<PagedResult<Product>, PagedResult<ProductViewModel>>();

    }
    
  }
  
}