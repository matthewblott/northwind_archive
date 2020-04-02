namespace northwind.web.ui.tags
{
  using System;
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.Razor.TagHelpers;

  public abstract class BooleanTagHelper : TagHelper
  {
    private readonly string _attributeName;
    public abstract bool IsTrue { get; set; }

    protected BooleanTagHelper(string attributeName)
    {
      if (string.IsNullOrWhiteSpace(attributeName))
      {
        throw new NullReferenceException();
      }
      
      _attributeName = attributeName;
    }
    
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
      if (!IsTrue)
      {
        return;
      }
      
      // ReSharper disable once MustUseReturnValue
      await output.GetChildContentAsync();

      output.Attributes.Add(new TagHelperAttribute(_attributeName, true.ToString().ToLower()));
      
      base.Process(context, output);

    }
    
  }
  
}