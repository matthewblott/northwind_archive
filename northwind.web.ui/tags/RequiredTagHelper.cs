namespace northwind.web.ui.tags
{
  using Microsoft.AspNetCore.Razor.TagHelpers;

  [HtmlTargetElement("input", TagStructure = TagStructure.WithoutEndTag)]
  public class RequiredTagHelper : BooleanTagHelper
  {
    [HtmlAttributeName("nw-is-required")]
    public override bool IsTrue { get; set; }

    public RequiredTagHelper() : base("required") { }

  }
  
}