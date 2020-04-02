namespace northwind.web.ui.tags
{
  using Microsoft.AspNetCore.Razor.TagHelpers;

  [HtmlTargetElement("input", TagStructure = TagStructure.WithoutEndTag)]
  public class ReadOnlyTagHelper : BooleanTagHelper
  {
    [HtmlAttributeName("nw-is-readonly")]
    public override bool IsTrue { get; set; }

    public ReadOnlyTagHelper() : base("readonly") { }

  }
  
}