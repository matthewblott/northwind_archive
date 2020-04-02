namespace northwind.web.ui.tags
{
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.Razor.TagHelpers;

  [HtmlTargetElement("a")]
  [HtmlTargetElement("input", TagStructure = TagStructure.WithoutEndTag)]
  public class DisabledTagHelper : BooleanTagHelper
  {
    [HtmlAttributeName("nw-is-disabled")]
    public override bool IsTrue { get; set; }

    public DisabledTagHelper() : base("disabled") { }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
      if (context.IsHyperLink() && output.HasHref() && IsTrue)
      {
        output.Attributes.Remove(output.Href());
      }
      
      await base.ProcessAsync(context, output);

    }
    
  }

}