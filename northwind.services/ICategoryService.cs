namespace northwind.services
{
  using northwind.domain.models;
  using types;
  using common.data;
  using models.categories;
  
  public interface ICategoryService : IServiceActions<Category>
  {
    PagedData<CategoryServiceModel> Find(Pager pager, IQueryValues values, string orderBy = "", bool isDescending = false);
  }
}