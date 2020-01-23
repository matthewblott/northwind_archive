namespace northwind.web.ui.models
{
  public class CustomerModalViewModel
  {
    public string Id { get; set; }
    public string CompanyName { get; set; }
    public string Region { get; set; }
    public bool IsNew => string.IsNullOrWhiteSpace(Id);

    public string ElementId => IsNew ? "new" : Id;
    public string SaveText => IsNew ? "Create" : "Save";
    public string Title => IsNew ? "New" : "Edit";

  }
}