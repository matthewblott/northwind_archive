using System;
using System.Collections.Generic;

namespace northwind.domain.models
{
  public partial class Territories
  {
    public string Id { get; set; }
    public string TerritoryDescription { get; set; }
    public long RegionId { get; set; }
  }
}