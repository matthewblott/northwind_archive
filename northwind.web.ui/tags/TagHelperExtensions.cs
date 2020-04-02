namespace northwind.web.ui.tags
{
  using System;
  using System.IO;
  using System.Linq;
  using System.Text.Encodings.Web;
  using Microsoft.AspNetCore.Html;
  using Microsoft.AspNetCore.Mvc.Rendering;
  using Microsoft.AspNetCore.Razor.TagHelpers;

  public static class TagHelperExtensions
  {
    public static bool IsHyperLink(this TagHelperContext context) 
      => string.Equals(context.TagName, "a", StringComparison.OrdinalIgnoreCase);

    public static bool HasHref(this TagHelperOutput output) 
      => output.Attributes
        .Any(a => string.Equals(a.Name, "href", StringComparison.OrdinalIgnoreCase));

    public static TagHelperAttribute Href(this TagHelperOutput output) 
      => output
        .Attributes
        .SingleOrDefault(a => string.Equals(a.Name, "href", StringComparison.OrdinalIgnoreCase));
    
  }
  
}