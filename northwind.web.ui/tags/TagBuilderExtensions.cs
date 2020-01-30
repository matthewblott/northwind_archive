using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

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
      
      var newValue =  value.MakeHtml5("readonly").MakeHtml5("required").MakeHtml5("data-nw-validation");
        
      builder.AppendHtmlLine(newValue);
      
    }
    
    private static string MakeHtml5(this string str, string value) 
      => str.Replace($"{value}=\"{value}\"", value);

    public static void Add(this AttributeDictionary attributes, string key) 
      => attributes.Add(key, key);
    
    public static void AddIf(this AttributeDictionary attributes, string key, bool isValid)
    {
      if (isValid)
      {
        attributes.Add(key);
      }
      
    }

    public static void AddIf(this AttributeDictionary attributes, string key, string value, bool isValid)
    {
      if (isValid)
      {
        attributes.Add(key, value);
      }
      
    }
    
    public static void AddCssClassIf(this TagBuilder tag, string value, bool isValid)
    {
      if (isValid)
      {
        tag.AddCssClass(value); 
      }
      
    }
    
    public static T Foo<T>(this IEnumerable<object> attributes)
    {
      var type = typeof(T);
      var attribute = (T)attributes.First(x => x.GetType() == type);

      return attribute;

    }
    
    public static void Add(this AttributeDictionary attributes, string key, int value) 
      => attributes.Add(key, value.ToString());

    public static void Add(this AttributeDictionary attributes, string key, bool value) 
      => attributes.Add(key, value.ToString().ToLower());

  }
  
}