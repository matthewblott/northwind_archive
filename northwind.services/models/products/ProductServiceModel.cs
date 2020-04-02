namespace northwind.services.models.products
{
  using common.mapping;
  using domain.models;
  
  public class ProductServiceModel : IMapFrom<Product>
  {
    public int Id { get; set; }
    public string ProductName { get; set; }
    public int SupplierId { get; set; }
    public int CategoryId { get; set; }
    public string QuantityPerUnit { get; set; }
    public decimal UnitPrice { get; set; }
    public int UnitsInStock { get; set; }
    public int UnitsOnOrder { get; set; }
    public int ReorderLevel { get; set; }
    public int Discontinued { get; set; }
  }
}