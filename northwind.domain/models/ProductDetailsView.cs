﻿namespace northwind.domain.models
{
  public class ProductDetailsView
  {
    public int? Id { get; set; }
    public string ProductName { get; set; }
    public int? SupplierId { get; set; }
    public int? CategoryId { get; set; }
    public string QuantityPerUnit { get; set; }
    public decimal UnitPrice { get; set; }
    public int? UnitsInStock { get; set; }
    public int? UnitsOnOrder { get; set; }
    public int? ReorderLevel { get; set; }
    public int? Discontinued { get; set; }
    public string CategoryName { get; set; }
    public string CategoryDescription { get; set; }
    public string SupplierName { get; set; }
    public string SupplierRegion { get; set; }
  }
}