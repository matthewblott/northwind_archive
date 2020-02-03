using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore.Internal;

namespace northwind.web.ui.tags
{
  public static class TagBuilderExtensions
  {
    public static void AppendHtml5(this IHtmlContentBuilder builder, TagBuilder tag)
    {
      IHtmlContent newTag = tag;

      if (tag.TagName == "input")
      {
        newTag = tag.RenderStartTag();
      }
      
      using var writer = new StringWriter();

      newTag.WriteTo(writer, HtmlEncoder.Default);

      var value = writer.ToString();
      
      var newValue =  value
        .MakeHtml5("readonly")
        .MakeHtml5("required")
        .MakeHtml5("selected")
        .MakeHtml5("disabled")
        .MakeHtml5("data-nw-validation");

      builder.AppendHtmlLine(newValue);
      
    }
    
    private static string MakeHtml5(this string str, string value) 
      => str.Replace($"{value}=\"{value}\"", value);

    public static void Add(this AttributeDictionary attributes, string key) 
      => attributes.Add(key, key);
    
    public static void AddIf(this AttributeDictionary attributes, bool isValid, string key)
    {
      if (isValid)
      {
        attributes.Add(key);
      }
      
    }

    public static void AddIf(this AttributeDictionary attributes, bool isValid, string key, string value)
    {
      if (isValid)
      {
        attributes.Add(key, value);
      }
      
    }
    
    public static void AddIfMissing(this AttributeDictionary attributes, string key, string value)
    {
      var any = attributes.Any(x => x.Key == "type");

      if (!any)
      {
        attributes.Add(key, value);
      }
      
    }

    public static void AddIfMissing(this AttributeDictionary attributes, string key, int value)
    {
      var any = attributes.Any(x => x.Key == key);

      if (!any)
      {
        attributes.Add(key, value);
      }
      
    }

    public static void AddCssClassIf(this TagBuilder tag, bool isValid, string value)
    {
      if (isValid)
      {
        tag.AddCssClass(value); 
      }

    }
    
    public static T GetModelAttribute<T>(this IEnumerable<object> attributes)
    {
      var type = typeof(T);
      var attribute = (T)attributes.FirstOrDefault(x => x.GetType() == type);

      return attribute;

    }
    
    public static void Add(this AttributeDictionary attributes, string key, int value) 
      => attributes.Add(key, value.ToString());

    public static void Add(this AttributeDictionary attributes, string key, bool value) 
      => attributes.Add(key, value.ToString().ToLower());

    public static bool HasCssClass(this AttributeDictionary attributes, string className)
    {
      var value = attributes.FirstOrDefault(x => x.Key == "class").Value;

      if (string.IsNullOrWhiteSpace(value))
      {
        return false;
      }

      var classNames = value.Split(" ");
      var any = classNames.Any(x => x == className);

      return any;

    }

    public static bool IsMissing(this AttributeDictionary attributes, string key) 
      => attributes.All(x => x.Key != key);
  }
  
}