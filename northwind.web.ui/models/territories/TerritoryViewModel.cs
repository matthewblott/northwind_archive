namespace northwind.web.ui.models.territories
{
  using System.ComponentModel.DataAnnotations;
  using RegionType = domain.types.Region;
  using common.mapping;
  using northwind.domain.models;  
  public class TerritoryViewModel : IMapFrom<Territory>, IMapTo<Territory>
  {
    [Required]
    public string Id { get; set; }
    
    [Required]
    public string Description { get; set; }
    
    [Required]
    public RegionType Region { get; set; }
    
    public bool IsNew => string.IsNullOrWhiteSpace(Id);
    
    public string ActionName => IsNew ? "Create" : "Update";
    
  }
  
}