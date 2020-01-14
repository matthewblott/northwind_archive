using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using northwind.domain;
using AutoMapper;
using northwind.services;

namespace northwind.web.ui.setup
{
  public static class StartupExtensions
  {
    public static void AddAutoMapper(this IServiceCollection services) => services.AddAutoMapper(typeof(Startup));

    public static void AddDomain(this IServiceCollection services, string connectionString)
    {
      var optionsBuilder = new DbContextOptionsBuilder();
      
      void OptionsRunner(DbContextOptionsBuilder builder)
      {
        builder.EnableSensitiveDataLogging();
        builder.UseSqlite(connectionString);
      }
      
      OptionsRunner(optionsBuilder);
      
      services.AddDbContextPool<Context>(OptionsRunner);
      services.AddScoped<IContext, Context>();

    }

    public static void AddDomainServices(this IServiceCollection services)
    {
      services.AddScoped<ICustomerService, CustomerService>();
    }
    
    public static void AddRouting(this IServiceCollection services) => 
      services.AddRouting(option => option.LowercaseUrls = true);

    public static void AddMvcServices(this IServiceCollection services) => 
      services.AddMvc().AddRazorRuntimeCompilation();
    
  }
  
}