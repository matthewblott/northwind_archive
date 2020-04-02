namespace northwind.services
{
  using cloudscribe.Pagination.Models;
  using common.data;

  public interface IServiceActions<T> where T : class
  {
    PagedResult<T> Find(Pager pager);
    T Find(object id);
    int Create(T entity);
    bool Update(T entity);
    bool Delete(object id);
    bool Exists(object id);
    
  }
  
}