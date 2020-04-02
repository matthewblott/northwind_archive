namespace northwind.web.ui.setup
{
  using Microsoft.AspNetCore.Builder;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;
  
  public class Startup
  {
    public Startup(IConfiguration configuration) => Configuration = configuration;

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddUrlHelperServices();
      services.AddAutoMapper();
      services.AddLazyCache();
      services.AddCloudscribePagination();
      services.AddCloudscribeNavigation();
      services.AddDomain();
      services.AddConventionalServices();
      services.AddReporting();
      services.AddRoutingOptions();
      services.AddMvcServices(Configuration.GetRazorSettings());
    }

    public void Configure(IApplicationBuilder app)
    {
      app.UseDeveloperExceptionPage();
      app.UseStaticFiles();
      app.UseRouting();
      app.UseCors();
      app.UseEndpoints();
    }
    
  }
}
