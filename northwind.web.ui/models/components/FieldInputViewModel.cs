namespace northwind.web.ui.models.components
{
  using System;
  using Humanizer;
  using Microsoft.AspNetCore.Mvc.ViewFeatures;

  public class FieldInputViewModel
  {
    public string Id => Guid.NewGuid().ToString();
    public string Name { get; set; }
    public string DisplayName => Name.Humanize().Titleize();
    public string Placeholder => Name.Humanize().Titleize();
    public InputType InputType { get; set; }
    public object Value { get; set; }
    public bool IsReadOnly { get; set; }
    public bool IsRequired { get; set; }
    public bool IsValidation { get; set; }
    public bool IsChecked { get; set; }
    
    public string ValidationAttributeString => IsValidation ? "data-nw-validation" : string.Empty;

  }
  
}