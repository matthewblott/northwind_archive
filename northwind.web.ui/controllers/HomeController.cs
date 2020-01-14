using Microsoft.AspNetCore.Mvc;

namespace northwind.web.ui.controllers
{
  public class HomeController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }

  }
    
}
