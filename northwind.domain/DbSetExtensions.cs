namespace northwind.domain
{
  using System.Collections.Generic;
  using System.IO;
  using System.Reflection;
  using Humanizer;
  using Microsoft.Data.SqlClient;
  using Microsoft.Data.Sqlite;
  using Microsoft.EntityFrameworkCore;
  using models.queries;
  
  public static class DbSetExtensions
  {
    // public static IEnumerable<RecentOrder> FindByCustomer(this DbSet<RecentOrder> value, string customerId) 
    //   => value.FromSqlRaw(GetSqlRaw("RecentOrders"), new SqliteParameter(nameof(customerId), customerId));
    
    public static IEnumerable<T> FindByCustomer<T>(this DbSet<T> value, string customerId) where T : class
    {
      var name = "Recent" + typeof(T).Name.Pluralize();
      
      return value.FromSqlRaw(GetSqlRaw(name), new SqlParameter(nameof(customerId), customerId));
    }


    private static string GetSqlRaw(string name)
    {
      var stream = Assembly.GetExecutingAssembly()
        .GetManifestResourceStream($"northwind.domain.sql.{name}.sql");

      if (stream == null)
        return string.Empty;

      using var reader = new StreamReader(stream);
      return reader.ReadToEnd();
      
    }
    
  }
  
}