using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using cloudscribe.Pagination.Models;
using northwind.services.types;

namespace northwind.services
{
  public interface IServiceActions<T> where T : class
  {
    PagedResult<T> Find(Pager pager, IQueryParameters parameters);
    PagedResult<T> Find(Pager pager);
    PagedResult<T> Find(Pager pager, Expression<Func<T, bool>> expression);
    IEnumerable<T> Find(IQueryParameters parameters);
    T Find(params object[] keyValues);
    int Create(T entity);
    bool Update(T entity);
    bool Delete(params object[] keyValues);
    bool Exists(params object[] keyValues);
    bool DoesNotExist(params object[] keyValues);
    
  }
  
}