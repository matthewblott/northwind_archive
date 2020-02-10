using Microsoft.AspNetCore.Mvc;
using northwind.reporting;

namespace northwind.web.ui.controllers
{
  public class ReportsController : Controller
  {
    private readonly IReportService _reportService;

    public ReportsController(IReportService reportService)
    {
      _reportService = reportService;
    }

    public IActionResult Index()
    {
      var model = new { Name = "John Doe" };
      
      ViewBag.File = _reportService.CreateReport("Example1", model);
      
      return View();
      
    }
    
  }
  
}