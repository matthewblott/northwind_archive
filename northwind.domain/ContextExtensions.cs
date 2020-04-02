namespace northwind.domain
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata;
  using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
  using models;
  using models.queries;
  using types;

  public static class ContextExtensions
  {
    public static void Build(this ModelBuilder builder)
    {
      builder.Entity<Category>(entity =>
      {        
        entity.HasKey(e => e.Id);
      });

      builder.Entity<Customer>(entity =>
      {
        entity.HasKey(e => e.Id);
      });

      builder.Entity<EmployeeTerritory>(entity =>
      {
      });

      builder.Entity<Employee>(entity =>
      {
        entity.Property(e => e.Id);
      });

      builder.Entity<OrderDetails>(entity =>
      {
        entity.Property(e => e.Discount).HasColumnType(nameof(Double));
        entity.Property(e => e.UnitPrice).IsRequired().HasColumnType(nameof(Decimal));
      });

      builder.Entity<Order>(entity =>
      {
        entity.Property(e => e.Id);
        entity.Property(e => e.OrderDate)
          .HasConversion(new DateTimeToStringConverter());
        entity
          .HasMany(o => o.OrderDetails)
          .WithOne(d => d.Order)
          .HasForeignKey(x => x.OrderId);
      });
      
      builder.Entity<ProductDetailsView>(entity =>
      {
        entity.HasNoKey();
        entity.ToView("ProductDetailsView");
        entity.Property(e => e.UnitPrice).HasColumnType(nameof(Decimal));
      });

      builder.Entity<Product>(entity =>
      {        
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id).HasColumnType(nameof(Int32));
      });

      builder.Entity<Shipper>(entity =>
      {
        entity.Property(e => e.Id);
      });

      builder.Entity<Supplier>(entity =>
      {
        entity.Property(e => e.Id).ValueGeneratedNever();
      });

      builder.Entity<Territory>(entity =>
      {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Description)
          .HasColumnName("TerritoryDescription");
        entity.Property(e => e.Region)
          .HasColumnName("RegionId")
          .HasConversion(new EnumToNumberConverter<Region, int>());
      });
    }

    public static void BuildReports(this ModelBuilder builder)
    {
      builder.Entity<RecentOrder>().HasNoKey(); 
      builder.Entity<RecentOrderItem>().HasNoKey();
    }
    
    public static IEnumerable<IProperty> Keys(this IModel model, Type type) =>
      model.FindEntityType(type).FindPrimaryKey().Properties.ToList();
    
  }
  
}