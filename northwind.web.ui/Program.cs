using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using northwind.web.ui.setup;

namespace northwind.web.ui
{
  public class Program
  {
    public static void Main(string[] args)
    {
      CreateWebHostBuilder(args).Build().Run();
    }

    private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
      WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
  }
}
