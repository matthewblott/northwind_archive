namespace northwind.services
{
  using System.Collections.Generic;
  using domain.models;
  using common.data;
  using types;
  using models.orders;
  
  public interface IOrderService : IServiceActions<Order>
  {
    PagedData<OrderServiceModel> Find(Pager pager, IQueryValues values, string orderBy = "", bool isDescending = false);
    IEnumerable<OrderServiceModel> Find(SearchServiceModel model);
    
  }
  
}