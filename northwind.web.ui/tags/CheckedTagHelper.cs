namespace northwind.web.ui.tags
{
  using Microsoft.AspNetCore.Razor.TagHelpers;

  [HtmlTargetElement("input", TagStructure = TagStructure.WithoutEndTag)]
  public class CheckedTagHelper : BooleanTagHelper
  {    
    [HtmlAttributeName("nw-is-checked")]
    public override bool IsTrue { get; set; }

    public CheckedTagHelper() : base("checked") { }
    
  }
  
}