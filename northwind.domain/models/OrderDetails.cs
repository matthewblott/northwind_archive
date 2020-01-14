using System;
using System.Collections.Generic;

namespace northwind.domain.models
{
  public class OrderDetails
  {
    public string Id { get; set; }
    public long OrderId { get; set; }
    public long ProductId { get; set; }
    public byte[] UnitPrice { get; set; }
    public long Quantity { get; set; }
    public double Discount { get; set; }
  }
}