using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using cloudscribe.Pagination.Models;
using northwind.services.types;

namespace northwind.services
{
  public static class QueryableExtensions
  {
    public static PagedResult<T> GetPaged<T>(this IQueryable<T> query, Page page) where T : class
    {
      var pageNumber = page.PageNumber;
      var pageSize = page.PageSize;
      var currentPageNum = pageNumber;
      var offset = (pageSize * currentPageNum) - pageSize;
      var offset0 = offset > int.MaxValue ? 0 : (int)offset;

      var data0 = new List<T>();
      
      try
      {
        var data = query.Skip(offset0).Take(pageSize).ToList();

        data0 = data;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
      
      var totalItems = query.Count();
      
      var pagedResult = new PagedResult<T>
      {
        Data = data0,
        PageNumber = currentPageNum,
        PageSize = pageSize,
        TotalItems = totalItems
      };

      return pagedResult;
      
    }

    public static PagedResult<T> GetPaged<T>(this IQueryable<T> query,
      Expression<Func<T, bool>> predicate, Page page) where T : class
    {
      var pageNumber = page.PageNumber;
      var pageSize = page.PageSize;
      var currentPageNum = pageNumber;
      var offset = (pageSize * currentPageNum) - pageSize;
      var query0 =  query.Where(predicate);
      var offset0 = offset > int.MaxValue ? 0 : (int)offset;
      
      var pagedResult = new PagedResult<T>
      {
        Data = query0
          .Where(predicate)
          .Skip(offset0)
          .Take(pageSize)
          .ToList(),
        PageNumber = currentPageNum,
        PageSize = pageSize,
        TotalItems = query0.Count()
      };

      return pagedResult;
      
    }

    
  }

}