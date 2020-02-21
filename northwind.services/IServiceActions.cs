namespace northwind.services
{
  using cloudscribe.Pagination.Models;
  using common.data;

  public interface IServiceActions<T> where T : class
  {
    PagedResult<T> Find(Pager pager);
    T Find(params object[] keyValues);
    int Create(T entity);
    bool Update(T entity);
    bool Delete(params object[] keyValues);
    bool Exists(params object[] keyValues);
    
  }
  
}