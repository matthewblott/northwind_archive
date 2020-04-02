namespace northwind.domain.models.queries
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations.Schema;

  public class RecentOrder
  {
    public int Id { get; set; }
    public string CustomerId { get; set; }
    public int EmployeeId { get; set; }
    public DateTime OrderDate { get; set; }
    public string RequiredDate { get; set; }
    public string ShippedDate { get; set; }
    [NotMapped]
    public IEnumerable<RecentOrderItem> RecentOrderItems { get; set; }
  }
}