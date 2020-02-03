using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using northwind.domain;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using northwind.services;
using northwind.web.ui.filters;
  
namespace northwind.web.ui.setup
{
  public static class StartupExtensions
  {
    public static void AddAutoMapper(this IServiceCollection services) => services.AddAutoMapper(typeof(Startup));

    public static void AddDomain(this IServiceCollection services)
    {
      var optionsBuilder = new DbContextOptionsBuilder();

      const string connectionString = "DataSource=../data/northwind.db";
      
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
      services.AddScoped<IRegionService, RegionService>();
      services.AddScoped<ICategoryService, CategoryService>();
      services.AddScoped<ICustomerService, CustomerService>();
      services.AddScoped<IProductService, ProductService>();
    }
    
    public static void AddRouting(this IServiceCollection services)
      => services.AddRouting(option => option.LowercaseUrls = true);

    public static void AddMvcServices(this IServiceCollection services)
    {
      services.AddMvc(options =>
      {
        options.Filters.Add(typeof(Breadcrumbs));
      }).AddRazorRuntimeCompilation();
      
    }

    public static void AddUrlHelperServices(this IServiceCollection services)
    {
      services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
      services.AddScoped(x => {
        var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
        var factory = x.GetRequiredService<IUrlHelperFactory>();
        return factory.GetUrlHelper(actionContext);
      });
      
    }
    public static void UseEndpoints(this IApplicationBuilder app)
    {
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapDefaultControllerRoute();
      });      

    }
    
  }
  
}