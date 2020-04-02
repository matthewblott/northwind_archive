namespace northwind.domain.models
{
  using types;
  
  public class Territory
  {
    public string Id { get; set; }
    public string Description { get; set; }
    public Region Region { get; set; }
  }
}