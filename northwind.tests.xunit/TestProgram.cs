namespace northwind.tests.xunit
{
  using Microsoft.Extensions.DependencyInjection;
  using web;
  
  public class TestProgram : Program
  {
    public void ConfigureTestServices(IServiceCollection services)
    {
      ConfigureServices(services);
    }
    
  }
  
}