namespace northwind.web.ui.models.customers
{
  using common.mapping;
  using northwind.services.models.customers;
  
  public class CustomerModalViewModel : IMapFrom<CustomerServiceModel>
  {
    public string Id { get; set; }
    public string CompanyName { get; set; }
    public string Region { get; set; }
    public bool IsNew => string.IsNullOrWhiteSpace(Id);

    public string ElementId => IsNew ? "new" : Id;
    public string SaveText => IsNew ? "Create" : "Save";
    public string Title => IsNew ? "[New]" : Id;
    public string Action => IsNew ? "CreatePartial" : "UpdatePartial";

  }
  
}