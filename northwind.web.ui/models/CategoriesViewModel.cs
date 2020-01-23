using cloudscribe.Pagination.Models;

namespace northwind.web.ui.models
{
  public class CategoriesViewModel
  {
    public CategoryQueryParameters Parameters { get; }
    public PagedResult<CategoryViewModel> PagedResult { get; }

    public CategoriesViewModel(CategoryQueryParameters parameters, PagedResult<CategoryViewModel> pagedResult)
    {
      Parameters = parameters;
      PagedResult = pagedResult;
    }
    
  }
  
}