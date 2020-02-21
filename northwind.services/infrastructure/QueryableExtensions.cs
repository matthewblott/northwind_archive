namespace northwind.services.infrastructure
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using cloudscribe.Pagination.Models;
  using common.data;

  public static class QueryableExtensions
  {
    public static PagedResult<T> GetPaged<T>(this IQueryable<T> query, Pager pager) where T : class
    {
      var pageNumber = pager.PageNumber;
      var pageSize = pager.PageSize;
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
        // todo: logging
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
    
    public static PagedData<T> GetPagedData<T>(this IQueryable<T> query, Pager pager) where T : class
    {
      var pageNumber = pager.PageNumber;
      var pageSize = pager.PageSize;
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
        // todo: logging
        Console.WriteLine(ex.Message);
      }
      
      var totalItems = query.Count();
      
      var pagedData = new PagedData<T>(data0, new Pager(currentPageNum, totalItems));

      return pagedData;
      
    }

  }

}