using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using northwind.domain.models;

namespace northwind.domain
{
  public static class ContextExtensions
  {
    public static void Build(this ModelBuilder builder)
    {
      builder.Entity<Category>(entity =>
      {        
        entity.HasKey(e => e.Id);
        //entity.ToTable("Categories");
        // entity.Property(e => e.Id).ValueGeneratedNever();
        // entity.Property(e => e.CategoryName).HasColumnType("VARCHAR(8000)");
        // entity.Property(e => e.Description).HasColumnType("VARCHAR(8000)");
      });

      builder.Entity<CustomerDemographics>(entity =>
      {
        entity.Property(e => e.Id).HasColumnType("VARCHAR(8000)");
        entity.Property(e => e.CustomerDesc).HasColumnType("VARCHAR(8000)");
      });
      
      builder.Entity<Customer>(entity =>
      {
        entity.HasKey(e => e.Id);
        // entity.Property(e => e.Id).HasColumnType("VARCHAR(8000)");
        // entity.Property(e => e.Address).HasColumnType("VARCHAR(8000)");
        // entity.Property(e => e.City).HasColumnType("VARCHAR(8000)");
        // entity.Property(e => e.CompanyName).HasColumnType("VARCHAR(8000)");
        // entity.Property(e => e.ContactName).HasColumnType("VARCHAR(8000)");
        // entity.Property(e => e.ContactTitle).HasColumnType("VARCHAR(8000)");
        // entity.Property(e => e.Country).HasColumnType("VARCHAR(8000)");
        // entity.Property(e => e.Fax).HasColumnType("VARCHAR(8000)");
        // entity.Property(e => e.Phone).HasColumnType("VARCHAR(8000)");
        // entity.Property(e => e.PostalCode).HasColumnType("VARCHAR(8000)");
        // entity.Property(e => e.Region).HasColumnType("VARCHAR(8000)");
      });

      builder.Entity<EmployeeTerritory>(entity =>
      {
        entity.Property(e => e.Id).HasColumnType("VARCHAR(8000)");
        entity.Property(e => e.TerritoryId).HasColumnType("VARCHAR(8000)");
      });

      builder.Entity<Employee>(entity =>
      {
        entity.Property(e => e.Id).ValueGeneratedNever();
        entity.Property(e => e.Address).HasColumnType("VARCHAR(8000)");
        entity.Property(e => e.BirthDate).HasColumnType("VARCHAR(8000)");
        entity.Property(e => e.City).HasColumnType("VARCHAR(8000)");
        entity.Property(e => e.Country).HasColumnType("VARCHAR(8000)");
        entity.Property(e => e.Extension).HasColumnType("VARCHAR(8000)");
        entity.Property(e => e.FirstName).HasColumnType("VARCHAR(8000)");
        entity.Property(e => e.HireDate).HasColumnType("VARCHAR(8000)");
        entity.Property(e => e.HomePhone).HasColumnType("VARCHAR(8000)");
        entity.Property(e => e.LastName).HasColumnType("VARCHAR(8000)");
        entity.Property(e => e.Notes).HasColumnType("VARCHAR(8000)");
        entity.Property(e => e.PhotoPath).HasColumnType("VARCHAR(8000)");
        entity.Property(e => e.PostalCode).HasColumnType("VARCHAR(8000)");
        entity.Property(e => e.Region).HasColumnType("VARCHAR(8000)");
        entity.Property(e => e.Title).HasColumnType("VARCHAR(8000)");
        entity.Property(e => e.TitleOfCourtesy).HasColumnType("VARCHAR(8000)");
      });

      builder.Entity<OrderDetails>(entity =>
      {
        entity.Property(e => e.Id).HasColumnType("VARCHAR(8000)");
        entity.Property(e => e.Discount).HasColumnType("DOUBLE");
        entity.Property(e => e.UnitPrice).IsRequired().HasColumnType("DECIMAL");
      });

      builder.Entity<Order>(entity =>
      {
        entity.Property(e => e.Id).ValueGeneratedNever();
        entity.Property(e => e.CustomerId).HasColumnType("VARCHAR(8000)");
        entity.Property(e => e.Freight).IsRequired().HasColumnType("DECIMAL");
        entity.Property(e => e.OrderDate).HasColumnType("VARCHAR(8000)");
        entity.Property(e => e.RequiredDate).HasColumnType("VARCHAR(8000)");
        entity.Property(e => e.ShipAddress).HasColumnType("VARCHAR(8000)");
        entity.Property(e => e.ShipCity).HasColumnType("VARCHAR(8000)");
        entity.Property(e => e.ShipCountry).HasColumnType("VARCHAR(8000)");
        entity.Property(e => e.ShipName).HasColumnType("VARCHAR(8000)");
        entity.Property(e => e.ShipPostalCode).HasColumnType("VARCHAR(8000)");
        entity.Property(e => e.ShipRegion).HasColumnType("VARCHAR(8000)");
        entity.Property(e => e.ShippedDate).HasColumnType("VARCHAR(8000)");
      });

      builder.Entity<ProductDetailsView>(entity =>
      {
        entity.HasNoKey();
        entity.ToView("ProductDetailsView");
        entity.Property(e => e.CategoryDescription).HasColumnType("VARCHAR(8000)");
        entity.Property(e => e.CategoryName).HasColumnType("VARCHAR(8000)");
        entity.Property(e => e.ProductName).HasColumnType("VARCHAR(8000)");
        entity.Property(e => e.QuantityPerUnit).HasColumnType("VARCHAR(8000)");
        entity.Property(e => e.SupplierName).HasColumnType("VARCHAR(8000)");
        entity.Property(e => e.SupplierRegion).HasColumnType("VARCHAR(8000)");
        entity.Property(e => e.UnitPrice).HasColumnType("DECIMAL");
      });

      builder.Entity<Product>(entity =>
      {        
        entity.HasKey(e => e.Id);
        // entity.Property(e => e.Id).ValueGeneratedNever();
        // entity.Property(e => e.ProductName).HasColumnType("VARCHAR(8000)");
        // entity.Property(e => e.QuantityPerUnit).HasColumnType("VARCHAR(8000)");
        // entity.Property(e => e.UnitPrice).IsRequired().HasColumnType("DECIMAL");
      });

      builder.Entity<Region>(entity =>
      {
        entity.Property(e => e.Id).ValueGeneratedNever();
        entity.Property(e => e.RegionDescription).HasColumnType("VARCHAR(8000)");
      });

      builder.Entity<Shipper>(entity =>
      {
        entity.Property(e => e.Id).ValueGeneratedNever();
        entity.Property(e => e.CompanyName).HasColumnType("VARCHAR(8000)");
        entity.Property(e => e.Phone).HasColumnType("VARCHAR(8000)");
      });

      builder.Entity<Supplier>(entity =>
      {
        entity.Property(e => e.Id).ValueGeneratedNever();
        entity.Property(e => e.Address).HasColumnType("VARCHAR(8000)");
        entity.Property(e => e.City).HasColumnType("VARCHAR(8000)");
        entity.Property(e => e.CompanyName).HasColumnType("VARCHAR(8000)");
        entity.Property(e => e.ContactName).HasColumnType("VARCHAR(8000)");
        entity.Property(e => e.ContactTitle).HasColumnType("VARCHAR(8000)");
        entity.Property(e => e.Country).HasColumnType("VARCHAR(8000)");
        entity.Property(e => e.Fax).HasColumnType("VARCHAR(8000)");
        entity.Property(e => e.HomePage).HasColumnType("VARCHAR(8000)");
        entity.Property(e => e.Phone).HasColumnType("VARCHAR(8000)");
        entity.Property(e => e.PostalCode).HasColumnType("VARCHAR(8000)");
        entity.Property(e => e.Region).HasColumnType("VARCHAR(8000)");
      });

      builder.Entity<Territory>(entity =>
      {
        entity.Property(e => e.Id).HasColumnType("VARCHAR(8000)");
        entity.Property(e => e.TerritoryDescription).HasColumnType("VARCHAR(8000)");
      });
    }

    public static IEnumerable<IProperty> Keys(this IModel model, Type type) =>
      model.FindEntityType(type).FindPrimaryKey().Properties.ToList();
    
  }
}