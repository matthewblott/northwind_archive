using Microsoft.AspNetCore.Mvc;
using northwind.web.ui.models;

namespace northwind.web.ui.controllers
{
  public class HomeController : Controller
  {
    public IActionResult Index() => View();
    public IActionResult Test() => View(new TestViewModel { StringProperty = "Hello World!" });

  }
    
}
