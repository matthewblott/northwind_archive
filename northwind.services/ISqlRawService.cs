namespace northwind.services
{
  using System.Collections.Generic;
  using models.sql;

  public interface ISqlRawService
  {
    IEnumerable<RecentOrderServiceModel> FindRecentOrders(string customerId);
  }
}