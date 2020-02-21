namespace northwind.services.models.territories
{
  using common.mapping;
  using domain.models;
  using RegionType = domain.types.Region;
  public class TerritoryServiceModel : IMapFrom<Territory>
  {
    public string Id { get; set; }
    public string Description { get; set; }
    public RegionType Region { get; set; }
  }
}