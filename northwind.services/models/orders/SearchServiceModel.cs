namespace northwind.services.models.orders
{
  using System;

  public class SearchServiceModel
  {
    public int EmployeeId { get; set; }
    public string CustomerId { get; set; }
    public DateTime? OrderDateFrom { get; set; }
    public DateTime? OrderDateTo { get; set; }

  }
}