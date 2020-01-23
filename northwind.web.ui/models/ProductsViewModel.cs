using cloudscribe.Pagination.Models;
using northwind.services;

namespace northwind.web.ui.models
{
  public class ProductsViewModel
  {
    public ProductQueryParameters Parameters { get; }
    public PagedResult<ProductViewModel> PagedResult { get; }
    
    public ProductsViewModel(ProductQueryParameters parameters, PagedResult<ProductViewModel> pagedResult)
    {
      Parameters = parameters;
      PagedResult = pagedResult;
    }

  }
}