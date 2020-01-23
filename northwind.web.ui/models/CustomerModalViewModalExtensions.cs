namespace northwind.web.ui.models
{
  public static class CustomerModalViewModalExtensions
  {
    public static CustomerModalViewModel ToModal(this PartialCustomerViewModel model)
    {
      var newModel = new CustomerModalViewModel
      {
        Id = model.Id,
        CompanyName = model.CompanyName,
        Region = model.Region,
      };

      return newModel;

    }
    
  }
  
}