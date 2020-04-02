namespace northwind.web.ui.models.categories
{
  using common.mapping;
  using domain.models;
  
  public class CategoryViewModel : IMapFrom<Category>, IMapTo<Category>
  {
    public int Id { get; set; }
    public string CategoryName { get; set; }
    public string Description { get; set; }
  }
}