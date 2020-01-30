using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using northwind.web.ui.validation;

namespace northwind.web.ui.models
{
  public class CustomerViewModel
  {
    [Required]
    [StringLength(6, MinimumLength = 1)]
    [Remote(action: "IdExists", controller:"Customers")]
    public string Id { get; set; }

    [Required]
    [Display(Name = "Company", Prompt = "Company")]
    public string CompanyName { get; set; }
    
    [Display(Name = "Contact", Prompt = "Contact")]
    public string ContactName { get; set; }
    public string ContactTitle { get; set; }
    public string Address { get; set; }
    public string City { get; set; }

    [ReadOnly(true)]
    public string Region { get; set; } = "British Isles";  
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string Phone { get; set; }
    public string Fax { get; set; }

    public bool IsNew => string.IsNullOrWhiteSpace(Id);
    
    public string ActionName => IsNew ? "Create" : "Update";

  }
  
}