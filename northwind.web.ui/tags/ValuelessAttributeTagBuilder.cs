namespace northwind.web.ui.tags
{
  using System;
  using System.Collections.Generic;
  using System.Text;
  using System.Web;
  using Microsoft.AspNetCore.Mvc.Rendering;
  
  public class ValuelessAttributeTagBuilder : TagBuilder
  {
    public List<string> ValuelessAttributes { get; private set; }

    public ValuelessAttributeTagBuilder(string tagName) : base(tagName)
    {
      ValuelessAttributes = new List<string>();
    }

    public void AddValuelessAttribute(string value)
    {
      if (ValuelessAttributes.Contains(value))
        ValuelessAttributes.Add(value);
    }

    public string ToString(TagRenderMode renderMode)
    {
      var sb = new StringBuilder();
      
      switch (renderMode)
      {
        case TagRenderMode.StartTag:
          sb.Append('<').Append(TagName);
          AppendAttributes(sb);
          AppendValuelessAttributes(sb);
          sb.Append('>');
          break;
        case TagRenderMode.EndTag:
          sb.Append("</").Append(TagName).Append('>');
          break;
        case TagRenderMode.SelfClosing:
          sb.Append('<').Append(TagName);
          AppendAttributes(sb);
          AppendValuelessAttributes(sb);
          sb.Append(" />");
          break;
        default:
          sb.Append('<').Append(TagName);
          AppendAttributes(sb);
          AppendValuelessAttributes(sb);
          sb.Append('>').Append(InnerHtml).Append("</").Append(TagName).Append('>');
          break;
      }

      return sb.ToString();
    }

    private void AppendAttributes(StringBuilder sb)
    {
      foreach (var keyValuePair in (IEnumerable<KeyValuePair<string, string>>) Attributes)
      {
        var key = keyValuePair.Key;
        
        if (!string.Equals(key, "id", StringComparison.Ordinal) || !string.IsNullOrEmpty(keyValuePair.Value))
        {
          var str = HttpUtility.HtmlAttributeEncode(keyValuePair.Value);
          
          sb.Append(' ').Append(key).Append("=\"").Append(str).Append('"');
          
        }
        
      }
      
    }

    private void AppendValuelessAttributes(StringBuilder sb)
    {
      foreach (var v in ValuelessAttributes)
      {
        var str = HttpUtility.HtmlAttributeEncode(v);
        
        sb.Append(' ').Append(str);
        
      }
      
    }
    
  }
  
}