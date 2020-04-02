namespace northwind.web.ui.tags
{
  using System.Text.Encodings.Web;
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.Mvc.TagHelpers;
  using Microsoft.AspNetCore.Razor.TagHelpers;

  [HtmlTargetElement("textarea", TagStructure = TagStructure.NormalOrSelfClosing)]
  [HtmlTargetElement("input", TagStructure = TagStructure.WithoutEndTag)]
  public class ValidationTagHelper : BooleanTagHelper
  {
    private const string AttributeName = "nw-is-validation";
    
    [HtmlAttributeName(AttributeName)]
    public override bool IsTrue { get; set; }

    public ValidationTagHelper() : base(AttributeName) { }
  
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
      if (IsTrue)
      {
        output.AddClass("validation", HtmlEncoder.Default);
      }
      
      await base.ProcessAsync(context, output);

    }
    
  }
  
}