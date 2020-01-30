namespace northwind.web.ui.models
{
  public class CustomerUpdatePartialViewModel
  {
    public string Id { get; set; }
    public string CompanyName { get; set; }
    public string Region { get; set; }
    public string ReturnUrl { get; set; }
    public bool IsNew => string.IsNullOrWhiteSpace(Id);
    
  }
  
}