namespace northwind.reporting.models
{
  using common.mapping;
  using domain.models.queries;

  public class RecentOrderItemViewModel : IMapFrom<RecentOrderItem>
  {
    public string Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public RecentOrder RecentOrder { get; set; }
  }
}