using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace northwind.web.ui.models.users
{
  public class UserViewModel
  {
    [Required]
    [StringLength(20, MinimumLength = 3)]
    public string Username { get; set; }

    [Required]
    [StringLength(10, MinimumLength = 1)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    [Required]
    [StringLength(10, MinimumLength = 1)]
    [NotMapped]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    public string ConfirmedPassword { get; set; }

  }
  
}