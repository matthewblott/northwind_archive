using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using northwind.domain.models;

namespace northwind.domain
{
  public interface IContext : IDbContext
  {
    DbSet<Category> Categories { get; }
    DbSet<CustomerDemographics> CustomerDemographics { get; }
    DbSet<Customer> Customers { get; }
    DbSet<EmployeeTerritory> EmployeeTerritories { get; }
    DbSet<Employee> Employees { get; }
    DbSet<OrderDetails> OrderDetails { get; }
    DbSet<Order> Orders { get; }
    DbSet<ProductDetailsView> ProductDetailsView { get; }
    DbSet<Product> Products { get; }
    DbSet<Region> Regions { get; }
    DbSet<Shipper> Shippers { get; }
    DbSet<Supplier> Suppliers { get; }
    DbSet<Territory> Territories { get; }

    void Commit();
    
    // Required for getting the keys for expression tree queries
    IEnumerable<IProperty> Keys(Type type);

  }
}