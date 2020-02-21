namespace northwind.domain.models
{
  using RegionType = types.Region;
  
  public class Territory
  {
    public string Id { get; set; }
    public string Description { get; set; }
    public RegionType Region { get; set; }
  }
}