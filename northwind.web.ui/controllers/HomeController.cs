namespace northwind.web.ui.controllers
{
  using Microsoft.AspNetCore.Mvc;

  public class HomeController : Controller
  {
    public IActionResult Index() => View();
  }
}