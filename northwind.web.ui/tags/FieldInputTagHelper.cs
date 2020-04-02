namespace northwind.web.ui.tags
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.ComponentModel.DataAnnotations;
  using System.Linq;
  using Humanizer;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
  using Microsoft.AspNetCore.Mvc.Rendering;
  using Microsoft.AspNetCore.Mvc.TagHelpers;
  using Microsoft.AspNetCore.Mvc.ViewFeatures;
  using Microsoft.AspNetCore.Razor.TagHelpers;
  using common;
  using RemoteAttribute = validation.RemoteAttribute;
  
  [HtmlTargetElement("nw-field-input")]
  [HtmlTargetElement("nw-field")]
  public class FieldInputTagHelper : InputTagHelper
  {
    private readonly IUrlHelper _urlHelper;

    public FieldInputTagHelper(IHtmlGenerator generator, IUrlHelper urlHelper) 
      : base(generator) => _urlHelper = urlHelper;

    [HtmlAttributeName("nw-is-readonly")]
    public bool IsReadOnly { get; set; }

    [HtmlAttributeName("nw-is-validation")]
    public bool IsValidation { get; set; } = true;

    public InputType InputType { get; set; } = InputType.Text;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      output.TagName = null;
      output.TagMode = TagMode.StartTagAndEndTag;

      var div1 = GetFieldTag();

      var label1 = GetLabelTag();

      div1.InnerHtml.AppendHtml(label1);

      var div2 = GetFieldBodyTag();

      div1.InnerHtml.AppendHtml(div2);

      var input1 = GetInputTag();
     
      // if it's a checkbox it needs to be wrapped in a span for display reasons
      if (IsBool)
      {
        var span1 = GetSpanTag();
        
        span1.InnerHtml.AppendHtml5(input1);
        span1.InnerHtml.AppendHtml5(GetCheckBoxFalseValueInputTag());
        
        div2.InnerHtml.AppendHtml5(span1);

      }
      else
      {
        div2.InnerHtml.AppendHtml5(input1);
      }

      output.Content.AppendHtml(div1);
      
    }

    private IEnumerable<object> GetModelAttributes()
      => (For.ModelExplorer.Metadata as DefaultModelMetadata)?.Attributes.Attributes;

    private bool IsOfType(Type type) => GetModelAttributes()?.Any(x => x.GetType() == type) ?? false;
    private bool IsRequired => IsOfType(typeof(RequiredAttribute));
    private bool IsReadOnlyResult => IsOfType(typeof(ReadOnlyAttribute)) || IsReadOnly;
    private bool IsRegularExpression => IsOfType(typeof(RegularExpressionAttribute));
    private bool IsEditable => IsOfType(typeof(EditableAttribute));
    private bool IsDataType => IsOfType(typeof(DataTypeAttribute));
    private bool IsCompare => IsOfType(typeof(CompareAttribute));
    private bool IsDisplay => IsOfType(typeof(DisplayAttribute));
    private bool IsStringLength => IsOfType(typeof(StringLengthAttribute));
    private bool IsMinLength => IsOfType(typeof(MinLengthAttribute));
    private bool IsMaxLength => IsOfType(typeof(MaxLengthAttribute));
    private bool IsRemote => IsOfType(typeof(RemoteAttribute));
    private bool IsBool => For.ModelExplorer.ModelType == typeof(bool);
    
    // todo: use the field type of the model so the DataTypeAttribute isn't required
    private bool IsDateTime 
      => IsDataType && GetModelAttributes().GetModelAttribute<DataTypeAttribute>().DataType == DataType.DateTime;
    private bool IsDate 
      => IsDataType && GetModelAttributes().GetModelAttribute<DataTypeAttribute>().DataType == DataType.Date;

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

    private TagBuilder GetCheckBoxFalseValueInputTag()
    {
      var input1 = new TagBuilder("input") { TagRenderMode = TagRenderMode.SelfClosing };

      input1.Attributes.Add("name", For.Name);
      input1.Attributes.Add("type", "hidden");
      input1.Attributes.Add("value", false.ToString().ToLower());
      
      return input1;
    }
    
    private TagBuilder GetInputTag()
    {
      var isNull = For.Model == null;

      var input1 = new TagBuilder("input") { TagRenderMode = TagRenderMode.SelfClosing };
      
      input1.AddCssClassIf(IsValidation,"validation");
      input1.Attributes.Add("id", For.Name);
      input1.Attributes.Add("name", For.Name);
      input1.Attributes.AddIf(IsReadOnlyResult,"readonly");
      input1.Attributes.AddIf(IsRequired, "required");
      input1.Attributes.AddIf(IsBool,"type", "checkbox");

      if (IsBool)
      {
        input1.Attributes.AddIf(Convert.ToBoolean(For.Model), "checked");
      }
      
      input1.Attributes.AddIf( !isNull && !IsBool, "value", $"{For.Model}");
      input1.Attributes.AddIf(IsBool, "value", true.ToString().ToLower());
      input1.Attributes.AddIfMissing("value", $"{For.Model}");
      
      input1.AddCssClassIf(IsBool,"checkbox");
      input1.Attributes.AddIf(!IsDisplay,"placeholder", For.Name.Humanize().Titleize());

      // Need to account for radio and textarea
      input1.AddCssClassIf(!IsBool,"input");
      
      if (IsDate && !IsReadOnlyResult)
      {
        input1.Attributes.Add("type", "date");
        input1.Attributes.Add("data-show-header", false);
        input1.Attributes.Add("data-color", "dark");
        input1.Attributes.Add("data-date-format", "YYYY-MM-DD"); 
        input1.MergeAttribute("value", string.Format(DateFormats.InternationalDate, For.Model), true);
      }

      if (IsDateTime && !IsReadOnlyResult)
      {
        input1.Attributes.Add("type", "date");
        input1.Attributes.Add("data-show-header", false);
        input1.Attributes.Add("data-color", "dark");
        input1.Attributes.Add("data-date-format", "YYYY-MM-DD HH:mm");
        input1.MergeAttribute("value", string.Format(DateFormats.InternationalDateTime, For.Model), true);
      }

      if (IsRegularExpression)
      {
        input1.Attributes.Add("pattern", GetModelAttributes().GetModelAttribute<RegularExpressionAttribute>().Pattern);
      }

      if (IsCompare)
      {
        input1.Attributes.Add("data-nw-compare",GetModelAttributes().GetModelAttribute<CompareAttribute>().OtherProperty);
      }

      if (IsEditable)
      {
        input1.Attributes.AddIf(!GetModelAttributes().GetModelAttribute<EditableAttribute>().AllowEdit, "readonly");
      }

      if (IsDisplay)
      {
        input1.Attributes.AddIf(!IsDate && !IsDateTime,"placeholder", GetModelAttributes().GetModelAttribute<DisplayAttribute>().Prompt);
        input1.Attributes.AddIf(IsDate,"placeholder",DateTime.Today.ToShortTimeString());
        input1.Attributes.AddIf(IsDateTime,"placeholder", DateTime.Today.ToShortTimeString());
      }
      
      if (IsStringLength)
      {
        var attribute = GetModelAttributes().GetModelAttribute<StringLengthAttribute>();

        input1.Attributes.Add("minlength", attribute.MinimumLength);
        input1.Attributes.Add("maxlength", attribute.MaximumLength);

      }
      
      if (IsMinLength)
      {
        input1.Attributes.AddIfMissing("minlength", GetModelAttributes().GetModelAttribute<MinLengthAttribute>().Length);
      }
      
      if (IsMaxLength)
      {
        input1.Attributes.AddIfMissing("maxlength", GetModelAttributes().GetModelAttribute<MaxLengthAttribute>().Length);
      }

      if (IsRemote)
      {
        var attribute = GetModelAttributes().GetModelAttribute<RemoteAttribute>();
        
        input1.Attributes.Add("data-nw-action", _urlHelper.ActionLink(attribute.Action, attribute.Controller));
        input1.Attributes.AddIf(!string.IsNullOrWhiteSpace(attribute.AdditionalFields), "data-nw-additional-fields", attribute.AdditionalFields);
        
      }
      
      input1.Attributes.Add("data-nw-validation");

      input1.Attributes.AddIfMissing("type", "text");
      
      return input1;
      
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

    private TagBuilder GetSpanTag()
    {
      var span1 = new TagBuilder("span");
      
      span1.AddCssClass("pt-1");
      
      return span1;
      
    }
    
  }
  
}