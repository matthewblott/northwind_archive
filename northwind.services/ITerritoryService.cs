namespace northwind.services
{
  using northwind.domain.models;
  using types;
  using common.data;
  using models.territories;
  
  public interface ITerritoryService : IServiceActions<Territory>
  {
    PagedData<TerritoryServiceModel> Find(Pager pager, IQueryValues values, string orderBy = "", bool isDescending = false);
  }
}