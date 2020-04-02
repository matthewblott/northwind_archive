namespace northwind.services.models.sql
{
  using System;
  using common.mapping;
  using domain.models.queries;

  public class RecentOrderServiceModel : IMapFrom<RecentOrder>
  {
    public int Id { get; set; }
    public string CustomerId { get; set; }
    public int EmployeeId { get; set; }
    public DateTime OrderDate { get; set; }
    public string RequiredDate { get; set; }
    public string ShippedDate { get; set; }
    
  }
}