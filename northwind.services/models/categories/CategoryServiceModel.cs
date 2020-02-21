namespace northwind.services.models.categories
{
  using common.mapping;
  using domain.models;

  public class CategoryServiceModel : IMapFrom<Category>
  {
    public long Id { get; set; }
    public string CategoryName { get; set; }
    public string Description { get; set; }
  }
}