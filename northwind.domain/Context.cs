using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using northwind.domain.models;

namespace northwind.domain
{
  public class Context : DbContext, IContext
  {
    public DbContext Instance => this;
    private IDbContextTransaction _transaction;

    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<EmployeeTerritory> EmployeeTerritories { get; set; }
    public virtual DbSet<Employee> Employees { get; set; }
    public virtual DbSet<OrderDetails> OrderDetails { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<ProductDetailsView> ProductDetailsView { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Region> Regions { get; set; }
    public virtual DbSet<Shipper> Shippers { get; set; }
    public virtual DbSet<Supplier> Suppliers { get; set; }
    public virtual DbSet<Territory> Territories { get; set; }

    public Context(DbContextOptions<Context> options) : base(options) => 
      _transaction = Instance.Database.BeginTransaction();
    
    public IEnumerable<IProperty> Keys(Type type) => Model.Keys(type);
    
    protected override void OnModelCreating(ModelBuilder builder) => builder.Build();

    public void Commit()
    {
      try
      {
        _transaction.Commit();
      }
      catch (Exception)
      {
        _transaction.Rollback();
      }
      finally
      {
        _transaction.Dispose();
        _transaction = Instance.Database.BeginTransaction();
      }
    }

    ~Context()
    {
      if (_transaction == null) return;

      _transaction.Dispose();
      _transaction = null;
      
    }
    
  }
  
}