namespace northwind.domain.models.queries
{
  using System.ComponentModel.DataAnnotations.Schema;

  public class RecentOrderItem
  {
    public string Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    [NotMapped]
    public RecentOrder RecentOrder { get; set; }
  }
}