namespace northwind.web.ui.setup
{
  using AutoMapper;
  using cloudscribe.Pagination.Models;
  using domain.models;
  using models;

  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      // Categories
      CreateMap<Category, CategoryViewModel>().ReverseMap();
      CreateMap<PagedResult<Category>, PagedResult<CategoryViewModel>>();

      // Products
      CreateMap<Product, ProductViewModel>().ReverseMap();
      CreateMap<PagedResult<Product>, PagedResult<ProductViewModel>>();
      
      // Regions
      CreateMap<Region, RegionViewModel>().ReverseMap();
      CreateMap<Region, RegionPartialViewModel>().ReverseMap();
      CreateMap<RegionPartialViewModel, RegionModalViewModel>();
      CreateMap<PagedResult<Region>, PagedResult<RegionPartialViewModel>>();
      
    }
    
  }
  
}