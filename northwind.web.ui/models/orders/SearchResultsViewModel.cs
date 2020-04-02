
namespace northwind.web.ui.models.orders
{
  using common.mapping;
  using System.Collections.Generic;
  using services.models.orders;
  
  public class SearchResultsViewModel : SearchViewModel, IMapFrom<SearchViewModel>
  {
    public IEnumerable<OrderServiceModel> Data { get; set; }
  }
}