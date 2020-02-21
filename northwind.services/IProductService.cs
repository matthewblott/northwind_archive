namespace northwind.services
{
  using common.data;
  using northwind.domain.models;
  using models.products;
  using types;

  public interface IProductService : IServiceActions<Product>
  {
    PagedData<ProductServiceModel> Find(Pager pager, IQueryValues values, string orderBy = "", bool isDescending = false);
  }
}