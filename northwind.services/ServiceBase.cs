using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using cloudscribe.Pagination.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using northwind.domain;
using northwind.services.types;

namespace northwind.services
{
public class ServiceBase<T> where T : class
  {
    protected readonly IContext _db;
    private readonly DbSet<T> _entities;
    protected ServiceBase(IContext db, DbSet<T> entities)
    {
      _db = db;
      _entities = entities;
    }
    private bool SaveChanges()
    {
      var message = string.Empty;
      
      try
      {
        _db.Instance.SaveChanges();
      }
      catch (DbUpdateException ex)
      {
        message = ex.InnerException?.Message;
      }
      catch (Exception ex)
      {
        message = ex.Message;
      }

      var success = string.IsNullOrWhiteSpace(message);
      
      return success;

    }
    private Expression<Func<T, bool>> GetExpresionTree(string field, object value)
    {
      // var expression = FindCachedExpression(key);
      //
      // if (expression != null)
      // {
      //   return (Func<T, bool>) expression;
      // }
      
      var toLower = typeof(string).GetMethod(nameof(string.ToLower), Type.EmptyTypes);
      var contains = typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) });
      var x = Expression.Parameter(typeof(T), "x");
      var prop0 = Expression.Property(x, field);
      var expr0 = Expression.Call(prop0, toLower);
      var arg0 = Expression.Constant(value, typeof(string));
      var expr1 = Expression.Call(arg0, toLower);
      var expr2 = Expression.Call(expr0, contains, expr1);
      var final = Expression.Lambda<Func<T, bool>>(expr2, x);

      // var compiled = final.Compile();
      // Cache.Add(key, compiled);
      
      return final;

    }

    // private static object FindCachedExpression(string key) => (
    //   from x in Cache where x.Key == key select x.Value).FirstOrDefault();
    
    private static Expression<Func<T, object>> GetOrderByExpression(string propertyName)
    {
      var param = Expression.Parameter(typeof(T), "x");
      
      Expression conversion = Expression.Convert(Expression.Property (param, propertyName), typeof(object));
      
      return Expression.Lambda<Func<T, object>>(conversion, param);
      
    }

    public IEnumerable<T> Find(IQueryParameters parameters) => Find(new Page(1), parameters).Data;

    public PagedResult<T> Find(Page page, IQueryParameters parameters)
    {
      var q = from x in _entities select x;

      var orderBy = parameters.OrderBy;
      
      // order by ...
      if (!string.IsNullOrWhiteSpace(orderBy) && !parameters.IsDescending)
      {
        var orderByExpression = GetOrderByExpression(orderBy);

        q = q.OrderBy(orderByExpression);

      }

      if (!string.IsNullOrWhiteSpace(orderBy) && parameters.IsDescending)
      {
        var orderByExpression = GetOrderByExpression(orderBy);

        q = q.OrderByDescending(orderByExpression);

      }
      
      var searchParams = parameters.GetType().GetProperties();

      // All non null parameters
      var nonNullParams = 
        from x in searchParams 
        where x.GetValue(parameters) != null 
        select x;

      // All string parameters
      var stringParams = 
        from x in searchParams 
        where x.PropertyType == typeof(string) 
        select x;

      // All empty string parameters
      var emptyStringParams = 
        from x in stringParams 
        where string.IsNullOrWhiteSpace((string) x.GetValue(parameters)) 
        select x;
      
      // Need to remove q2 (empty string parameters) from q0 (all non null parameters) so
      // we are only left with string characters with a value
      var validParams = nonNullParams.Where(x => emptyStringParams.All(x0 => x0.Name != x.Name));      
      
      // Don't include any parameters that can't be matched to fields for the specified type
      var typeFields = typeof(T).GetProperties();
      
      var typeFieldNames = 
        from x in typeFields 
        select x.Name;
      
      var q6 =
        from x in validParams
        where typeFieldNames.Contains(x.Name)
        select x;  
      
      foreach (var info in q6)
      {
        var field = info.Name;
        var value = info.GetValue(parameters);
        var expression = GetExpresionTree(field, value);

        q = q.Where(expression);

      }

      // This is the default if no sort is specified the query will sort on the primary key
      if (string.IsNullOrWhiteSpace(orderBy))
      {
        var keys = Keys();
      
        foreach (var key in keys)
        {
          if (typeFieldNames.Contains(key.Name))
          {
            var q2 = CreateSelectorExpression(key.Name);
          
            if (key.PropertyInfo.PropertyType == typeof(string))
            {
              var q3 = (Expression<Func<T, string>>)q2;
      
              q = q.OrderBy(q3);
            
            }
            else if (key.PropertyInfo.PropertyType == typeof(byte))
            {
              var q3 = (Expression<Func<T, byte>>)q2;
      
              q = q.OrderBy(q3);
            
            }
            else if (key.PropertyInfo.PropertyType == typeof(short))
            {
              var q3 = (Expression<Func<T, short>>)q2;
      
              q = q.OrderBy(q3);
            
            }
            else if (key.PropertyInfo.PropertyType == typeof(int))
            {
              var q3 = (Expression<Func<T, int>>)q2;
      
              q = q.OrderBy(q3);
            
            }
            else if (key.PropertyInfo.PropertyType == typeof(DateTime))
            {
              var q3 = (Expression<Func<T, DateTime>>)q2;
      
              q = q.OrderBy(q3);
            
            }
          
          }
        
        }
        
      }
      
      return q.GetPaged(page);

    }
    
    private static LambdaExpression CreateSelectorExpression(string property)
    {
      var r = Expression.Parameter(typeof(T));
      
      return Expression.Lambda(Expression.PropertyOrField(r, property), r);
      
    }
    
    public PagedResult<T> Find(Page page) =>  _entities?.GetPaged(page);
    
    public PagedResult<T> Find(Page page, Expression<Func<T, bool>> expression) => _entities?.GetPaged(expression, page);
    public T Find(params object[] keyValues) => _entities.Find(keyValues);
    public bool Exists(params object[] keyValues) => Find(keyValues) != null;
    public bool DoesNotExist(params object[] keyValues) => Find(keyValues) == null;
    protected int Create(T entity)
    {
      var entityEntry = _entities.Add(entity);
      var newEntity = entityEntry.Entity;

      SaveChanges();

      var props = newEntity.GetType().GetProperties();

      const string id = nameof(id);

      var q =
        from p in props
        where p.Name.ToLower() == id && p.PropertyType == typeof(int)
        select p;

      var propertyInfos = q as PropertyInfo[] ?? q.ToArray();
      
      if (!propertyInfos.Any())
      {
        return 0;
      }

      var idProperty = propertyInfos.FirstOrDefault();
      var idPropertyValue = idProperty?.GetValue(entity) ?? 0;
      var id0 = Convert.ToInt32(idPropertyValue);
      
      return id0;

    }
    protected bool Update(T entity)
    {
      _entities.Update(entity);
      
      return SaveChanges();

    }
    protected bool Delete(params object[] keyValues)
    {
      var entity = _entities.Find(keyValues);

      _entities.Remove(entity);

      SaveChanges();

      return SaveChanges();

    }
    private IEnumerable<IProperty> Keys() => Keys(typeof(T));
    private IEnumerable<IProperty> Keys(Type type) =>  _db.Keys(type);
    
  }
  
}