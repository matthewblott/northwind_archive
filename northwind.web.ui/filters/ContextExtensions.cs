using cloudscribe.Web.Navigation;
using Microsoft.AspNetCore.Http;

namespace northwind.web.ui.filters
{
  public static class ContextExtensions
  {
    public static void AdjustBreadcrumb(this HttpContext context, string key, string text, string url) =>
      new NavigationNodeAdjuster(context)
      {
        KeyToAdjust = key, AdjustedText = text, ViewFilterName = "Breadcrumbs", AdjustedUrl = url,
      }.AddToContext();

  }
  
}