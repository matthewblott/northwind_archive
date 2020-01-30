using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace northwind.web.ui.tags
{
  [HtmlTargetElement("nw-hidden")]
  public class HiddenTagHelper : InputTagHelper
  {
    public HiddenTagHelper(IHtmlGenerator generator) : base(generator)
    {
      
    }
    
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      output.TagName = null;
      output.TagMode = TagMode.StartTagAndEndTag;

      var div1 = GetFieldTag();

      output.Content.AppendHtml5(div1);
      
    }
    
    private TagBuilder GetFieldTag()
    {
      var input1 = new TagBuilder("input") { TagRenderMode = TagRenderMode.SelfClosing };

      input1.Attributes.Add("name", For.Name);
      input1.Attributes.Add("value", $"{For.Model}");
      input1.Attributes.Add("type", "hidden");

      return input1;
    }

  }
  
}