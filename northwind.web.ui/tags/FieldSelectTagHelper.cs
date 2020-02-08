using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Humanizer;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace northwind.web.ui.tags
{
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
    private bool IsOfType(Type type) => GetModelAttributes()?.Any(x => x.GetType() == type) ?? false;
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      if (Items == null)
      {
        throw new NullReferenceException();
      }
      
      output.TagName = null;
      output.TagMode = TagMode.StartTagAndEndTag;

      var div1 = GetFieldTag();

      var label1 = GetLabelTag();

      div1.InnerHtml.AppendHtml(label1);

      var div2 = GetFieldBodyTag();

      div1.InnerHtml.AppendHtml(div2);

      var select1 = GetSelectTag();
     
      div2.InnerHtml.AppendHtml5(select1);

      output.Content.AppendHtml(div1);

    }

    private TagBuilder GetSelectTag()
    {
      var select1 = new TagBuilder("select");

      select1.AddCssClass("select");
      select1.Attributes.Add("data-validation");
      select1.Attributes.Add("required");
      select1.Attributes.Add("id", For.Name);
      select1.Attributes.Add("name", For.Name);

      var option0 = new TagBuilder("option");
      var value = (string) For.Model;
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
      var div2 = new TagBuilder("div");

      div2.AddCssClass("control");
      div2.AddCssClass("is-expanded");
      div2.AddCssClass("field-body");

      return div2;
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