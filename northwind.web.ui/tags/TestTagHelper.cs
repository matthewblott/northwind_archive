using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace northwind.web.ui.tags
{
  [HtmlTargetElement("nw-test")]
  public class TestTagHelper : InputTagHelper
  {
    public TestTagHelper(IHtmlGenerator generator) : base(generator) { }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      output.TagName = null;
      output.TagMode = TagMode.StartTagAndEndTag;

      var div1 = new TagBuilder("div");

      div1.AddCssClass("div1");

      var tag = GetTag();

      div1.InnerHtml.AppendHtml5(tag);
        
      output.Content.AppendHtml(div1);

    }

    private TagBuilder GetTag()
    {
      var input1 = new TagBuilder("input") {TagRenderMode = TagRenderMode.StartTag};

      var type = InputTypeName?.ToLower();
      
      input1.Attributes.Add("readonly", null);
      // input1.Attributes.Add("type", type);
      input1.Attributes.Add("id", For.Name);
      input1.Attributes.Add("name", For.Name);
      input1.Attributes.Add("value", $"{For.Model}");
      
      return input1;

    }
    
  }
  
}