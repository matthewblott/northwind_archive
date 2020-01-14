using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Humanizer;

namespace northwind.web.ui.setup
{
  public static class HelperExtensions
  {
    public static HtmlString DisplayName<TClass>(
      this IHtmlHelper helper, Expression<Func<TClass, string>> expression)
    {
      var type = typeof(TClass);
      var metadata = helper.MetadataProvider.GetMetadataForType(type);

      var leftPart = expression.Body as MemberExpression;
      var fieldName = leftPart?.Member.Name;

      var q =
        from p in metadata.Properties
        where p.Name == fieldName
        select p;

      var displayName = q.FirstOrDefault()?.DisplayName;
      
      if (string.IsNullOrWhiteSpace(displayName))
      {
        displayName = fieldName.Humanize(LetterCasing.Title);
      }
      
      return new HtmlString(displayName);
      
    }

  }
  
}