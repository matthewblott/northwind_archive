namespace northwind.tests.web
{
  using Microsoft.AspNetCore;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.AspNetCore.Builder;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.Extensions.DependencyInjection;

  public class Program
  {
    public void ConfigureServices(IServiceCollection services)
    {
      var builder = services.AddMvc();
      
      builder.AddRazorRuntimeCompilation();
        
    }

    public void Configure(IApplicationBuilder app) 
      => app.UseRouting().UseEndpoints(p => p.MapDefaultControllerRoute());

    public static void Main() 
      => WebHost.CreateDefaultBuilder().UseStartup<Program>().Build().Run();
    
  }
  
  public class HomeController : Controller
  {
    public IActionResult Index() => Ok();
  }
  
}