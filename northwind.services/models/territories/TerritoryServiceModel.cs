namespace northwind.services.models.territories
{
  using common.mapping;
  using domain.models;
  using domain.types;
  public class TerritoryServiceModel : IMapFrom<Territory>
  {
    public string Id { get; set; }
    public string Description { get; set; }
    public Region Region { get; set; }
  }
}