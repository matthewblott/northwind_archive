using RazorLight.Extensions;

namespace northwind.web.ui.setup
{
  using AutoMapper;
  using DinkToPdf;
  using DinkToPdf.Contracts;
  using Microsoft.AspNetCore.Builder;
  using Microsoft.AspNetCore.Mvc.Infrastructure;
  using Microsoft.AspNetCore.Mvc.Routing;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.Extensions.Configuration;
  using domain;
  using filters;
  using reporting;
  using services;
  using services.implementations;

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
        // builder.UseInMemoryDatabase("northwind");
      }
      
      OptionsRunner(optionsBuilder);
      
      services.AddDbContextPool<Context>(OptionsRunner);
      
      // services
      //   .AddEntityFrameworkInMemoryDatabase()
      //   .AddDbContext<ToDoDbContext>(options =>
      //   {
      //     options.UseInMemoryDatabase(_connectionString, _databaseRoot);
      //     options.UseInternalServiceProvider(services.BuildServiceProvider());
      //   });
      
      // services.AddScoped<IContext, Context>();

    }

    public static void AddConventionalServices(this IServiceCollection services)
    {
      services.AddScoped<ICategoryService, CategoryService>();
      services.AddScoped<ICustomerService, CustomerService>();
      services.AddScoped<IDateTimeProvider, DateTimeProvider>();
      services.AddScoped<IOrderService, OrderService>();
      services.AddScoped<IProductService, ProductService>();
      services.AddScoped<ISqlRawService, SqlRawService>();
      services.AddScoped<ITerritoryService, TerritoryService>();
    }
    
    public static void AddRoutingOptions(this IServiceCollection services)
      => services.AddRouting(option => option.LowercaseUrls = true);

    public static void AddMvcServices(this IServiceCollection services, RazorSettings settings)
    {
      var builder = services.AddMvc(options =>
      {
        options.Filters.Add(typeof(Breadcrumbs));
      });

      var isAllowed = (settings ?? new RazorSettings()).AllowRuntimeCompilation;
      
      if (isAllowed)
      {
        builder.AddRazorRuntimeCompilation();
      }
  
    }

    public static RazorSettings GetRazorSettings(this IConfiguration configuration)
      => configuration.GetSection(nameof(RazorSettings)).Get<RazorSettings>();

    public static void AddReporting(this IServiceCollection services)
    {
      services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
      services.AddScoped<IReportService, ReportService>();
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