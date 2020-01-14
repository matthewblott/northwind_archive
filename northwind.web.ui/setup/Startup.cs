using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace northwind.web.ui.setup
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddAutoMapper();
      services.AddDomain("DataSource=../data/northwind.db");
      services.AddDomainServices();
      services.AddLazyCache();
      services.AddCloudscribePagination();
      services.AddRouting();
      services.AddMvcServices();

    }

    public void Configure(IApplicationBuilder app)
    {
      app.UseDeveloperExceptionPage();
      app.UseStaticFiles();
      app.UseRouting();
      app.UseCors();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapDefaultControllerRoute();
      });      
        
    }
    
  }
  
}
