using cloudscribe.Pagination.Models;
using northwind.services;

namespace northwind.web.ui.models
{
  public class RegionsViewModel
  {
    public RegionQueryParameters Parameters { get; }
    public PagedResult<RegionPartialViewModel> PagedResult { get; }
    public RegionsViewModel(RegionQueryParameters parameters, PagedResult<RegionPartialViewModel> pagedResult)
    {
      Parameters = parameters;
      PagedResult = pagedResult;
    }

  }
}