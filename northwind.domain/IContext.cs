using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using northwind.domain.models;

namespace northwind.domain
{
  public interface IContext : IDbContext
  {
    DbSet<Categories> Categories { get; }
    DbSet<CustomerDemographics> CustomerDemographics { get; }
    DbSet<Customer> Customers { get; }
    DbSet<EmployeeTerritories> EmployeeTerritories { get; }
    DbSet<Employees> Employees { get; }
    DbSet<OrderDetails> OrderDetails { get; }
    DbSet<Orders> Orders { get; }
    DbSet<ProductDetailsView> ProductDetailsView { get; }
    DbSet<Products> Products { get; }
    DbSet<Regions> Regions { get; }
    DbSet<Shippers> Shippers { get; }
    DbSet<Suppliers> Suppliers { get; }
    DbSet<Territories> Territories { get; }

    void Commit();
    
    // Required for getting the keys for expression tree queries
    IEnumerable<IProperty> Keys(Type type);

  }
}