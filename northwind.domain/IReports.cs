namespace northwind.domain
{
  using Microsoft.EntityFrameworkCore;
  using models.queries;

  public interface IReports
  {
    DbSet<RecentOrder> RecentOrders { get; }
    DbSet<RecentOrderItem> RecentOrderItems { get; set; }
  }
}