using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace northwind.web.ui.filters
{
  // To enable add the following in Startup and decorate the required action method with the Breadcrumbs attribute
  // services.AddScoped<Breadcrumbs>();
  
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
  public class BreadcrumbsAttribute : Attribute, IFilterFactory
  {
    public bool IsReusable => false;

    public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
    {
      var service = serviceProvider.GetService<Breadcrumbs>();
      
      return service;
    }
    
  }

}