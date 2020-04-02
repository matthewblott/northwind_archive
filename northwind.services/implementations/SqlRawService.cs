namespace northwind.services.implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
  using domain;
    using domain.models.queries;
    using models.sql;

  public class SqlRawService : ISqlRawService
  {
    private readonly IMapper _mapper;
    private readonly Context _db;
    
    public SqlRawService(Context db, IMapper mapper)
    {
      _db = db;
      _mapper = mapper;
    }
    
    public IEnumerable<RecentOrderServiceModel> FindRecentOrders(string customerId)
    {
      var orders = _db.RecentOrders.FindByCustomer(customerId);
      
      var items = _db.RecentOrderItems.FindByCustomer(customerId);

      
      
      foreach (var order in orders)
      {
        
        

      }
      
      
      var orders0 = _mapper.Map<IEnumerable<RecentOrderServiceModel>>(orders);
      
      return orders0;
    }
    
  }
  
}