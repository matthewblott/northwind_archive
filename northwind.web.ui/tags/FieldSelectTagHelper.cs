namespace northwind.web.ui.tags
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using System.Linq;
  using Humanizer;
  using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
  using Microsoft.AspNetCore.Mvc.Rendering;
  using Microsoft.AspNetCore.Mvc.ViewFeatures;
  using Microsoft.AspNetCore.Razor.TagHelpers;
  
  [HtmlTargetElement("nw-field-select")]
    public class FieldSelectTagHelper : TagHelper
  {
    private const string ForAttributeName = "asp-for";
    private const string ItemsAttributeName = "asp-items";
        
    [HtmlAttributeName(ForAttributeName)]
    public ModelExpression For { get; set; }

    [HtmlAttributeName(ItemsAttributeName)]
    public IEnumerable<SelectListItem> Items { get; set; }

    private bool IsDisplay => IsOfType(typeof(DisplayAttribute));
    private IEnumerable<object> GetModelAttributes()
      => (For.ModelExplorer.Metadata as DefaultModelMetadata)?.Attributes.Attributes;
    
    private bool IsRequired() => IsOfType(typeof(RequiredAttribute));

    private bool IsOfType(Type type) => GetModelAttributes()?.Any(x => x.GetType() == type) ?? false;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      output.TagName = null;
      output.TagMode = TagMode.StartTagAndEndTag;

      var field = GetFieldTag();
      var label = GetLabelTag();
      var fieldBody = GetFieldBodyTag();
      var selectWrapper = GetSelectWrapperTag();
      var select = GetSelectTag();

      selectWrapper.InnerHtml.AppendHtml5(select);
      fieldBody.InnerHtml.AppendHtml5(selectWrapper);
      
      field.InnerHtml.AppendHtml(label);
      field.InnerHtml.AppendHtml(fieldBody);
      
      output.Content.AppendHtml(field);

    }

    private TagBuilder GetSelectTag()
    {
      var select1 = new TagBuilder("select");

      select1.Attributes.Add("data-validation");
      select1.Attributes.AddIf(IsRequired(),"required");
      select1.Attributes.Add("id", For.Name);
      select1.Attributes.Add("name", For.Name);

      var option0 = new TagBuilder("option");
      var value = For.Model?.ToString();
      var any = Items.Any(x => x.Value == value);
      
      option0.Attributes.Add("disabled");
      option0.InnerHtml.Append("-- Select --");
      option0.Attributes.AddIf(!any, "selected");
      
      select1.InnerHtml.AppendHtml(option0);
      
      foreach (var item in Items)
      {
        var option1 = new TagBuilder("option");

        option1.Attributes.Add("value", item.Value);
        option1.Attributes.AddIf(item.Value == value, "selected"); 
        option1.InnerHtml.Append(item.Text);
        select1.InnerHtml.AppendHtml(option1);

      }

      return select1;

    }
    
    private TagBuilder GetFieldTag()
    {
      var div1 = new TagBuilder("div");

      div1.AddCssClass("field is-horizontal");

      return div1;
    }

    private TagBuilder GetFieldBodyTag()
    {
      var div1 = new TagBuilder("div");

      div1.AddCssClass("is-expanded");
      div1.AddCssClass("field-body");

      return div1;
    }

    private TagBuilder GetSelectWrapperTag()
    {
      var div1 = new TagBuilder("div");
      
      div1.AddCssClass("select");

      return div1;
    }
    
    private TagBuilder GetLabelTag()
    {
      var label1 = new TagBuilder("label");

      label1.AddCssClass("label");
      label1.AddCssClass("field-label");
      label1.AddCssClass("is-normal");
      label1.Attributes.Add("for", For.Name);
      
      var labelCaption = IsDisplay 
        ? GetModelAttributes().GetModelAttribute<DisplayAttribute>().Name : For.Name.Humanize().Titleize();
      
      label1.InnerHtml.Append(labelCaption);

      return label1;
    }
    
  }
      
}