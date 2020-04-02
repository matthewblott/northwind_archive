namespace northwind.web.ui.components
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.ComponentModel.DataAnnotations;
  using System.Linq;
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
  using Microsoft.AspNetCore.Mvc.ViewFeatures;
  using models.components;
  using tags;

  public class FieldSelectViewComponent : ViewComponent
  {
    private ModelExpression For;
    private IEnumerable<object> GetModelAttributes()
      => (For.ModelExplorer.Metadata as DefaultModelMetadata)?.Attributes.Attributes;

    private bool IsReadOnly;
    
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
    
    public async Task<IViewComponentResult> InvokeAsync(ModelExpression aspFor, bool isReadOnly = false)
    {
      For = aspFor;
      IsReadOnly = isReadOnly;

      var inputType = IsBool ? InputType.CheckBox : InputType.Text;
      var isNull = For.Model == null;
      
      var model = new FieldInputViewModel
      {
        Name = For.Name,
        InputType = inputType,
        Value = For.Model,
        IsReadOnly = isReadOnly
      };
      
      return View(model);
      
    }

  }
  
}