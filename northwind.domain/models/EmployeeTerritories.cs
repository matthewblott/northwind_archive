using System;
using System.Collections.Generic;

namespace northwind.domain
{
  public class EmployeeTerritories
  {
    public string Id { get; set; }
    public long EmployeeId { get; set; }
    public string TerritoryId { get; set; }
  }
}