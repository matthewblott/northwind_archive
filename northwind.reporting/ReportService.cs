using System;
using System.IO;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using RazorLight;

namespace northwind.reporting
{
  public class ReportService : IReportService
  {
    private readonly IConverter _converter;
    private readonly IWebHostEnvironment _environment;
    private readonly RazorLightEngine _engine;
    
    public ReportService(IConverter converter, IWebHostEnvironment environment)
    {
      _converter = converter;
      _environment = environment;

      var parentDir = Directory.GetParent(_environment.ContentRootPath);
      var currentDir = environment.ContentRootPath;
      var viewsDir = Path.Combine(currentDir, "views");

      if (_environment.IsDevelopment())
      {
        viewsDir = Path.Combine(parentDir.FullName, "northwind.reporting", "views");
      }
      
      _engine = new RazorLightEngineBuilder().UseFileSystemProject(viewsDir).Build();
      
    }
    
    public string CreateReport(string report, object model)
    {
      var result = _engine.CompileRenderAsync($"{report}.cshtml", model);
      var html = result.Result;
      var filename = Guid.NewGuid() + ".pdf";
      var path = Path.Combine(_environment.WebRootPath, "reports", filename);
      
      var doc = new HtmlToPdfDocument
      {
        GlobalSettings =
        {
          ColorMode = ColorMode.Color,
          Orientation = Orientation.Landscape,
          PaperSize = PaperKind.A4Plus,
          Out = path,
        },
        Objects =
        {
          new ObjectSettings
          {
            PagesCount = true,
            HtmlContent = html,
            WebSettings = {DefaultEncoding = "utf-8"},
            HeaderSettings = {FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812},
          }
        }
      };
      
      _converter.Convert(doc);

      return filename;

    }
    
  }
  
}