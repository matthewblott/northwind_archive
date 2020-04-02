namespace northwind.tests
{
  using web.ui.setup;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;
  using MyTested.AspNetCore.Mvc;
  using services;
  using mocks;
  
  public class TestStartup : Startup
  {
    public TestStartup(IConfiguration configuration) : base(configuration)
    {
      
    }

    public void ConfigureTestServices(IServiceCollection services)
    {
      // ConfigureServices(services);

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

      
      services.ReplaceTransient<IDateTimeProvider>(_ => DateTimeProviderMock.Instance);

    }
    
  }
  
}