namespace northwind.reporting.models
{
  using System;
  using System.Collections.Generic;
  using common.mapping;
  using domain.models.queries;

  public class RecentOrderViewModel : IMapFrom<RecentOrder>
  {
    public int Id { get; set; }
    public string CustomerId { get; set; }
    public int EmployeeId { get; set; }
    public DateTime OrderDate { get; set; }
    public string RequiredDate { get; set; }
    public string ShippedDate { get; set; }
    public IEnumerable<RecentOrderItemViewModel> RecentOrderDetails { get; set; }
  }
}