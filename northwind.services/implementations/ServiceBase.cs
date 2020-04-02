namespace northwind.services.implementations
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Linq.Expressions;
  using System.Reflection;
  using cloudscribe.Pagination.Models;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata;
  using common.data;
  using domain;
  using types;
  using infrastructure;
  
  public class ServiceBase<T> where T : class
  {
    protected readonly Context _db;
    private readonly DbSet<T> _entities;
    protected ServiceBase(Context db, DbSet<T> entities)
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

    private static Expression<Func<T, object>> GetOrderByExpression(string propertyName)
    {
      var param = Expression.Parameter(typeof(T), "x");
      
      Expression conversion = Expression.Convert(Expression.Property (param, propertyName), typeof(object));
      
      return Expression.Lambda<Func<T, object>>(conversion, param);
      
    }

    // todo: the pager argument needs amending, the TotalItems property isn't ideal because it's not required as an
    // input but it is required as an output when working with the result set.
    public PagedData<T> Find(Pager pager, IQueryValues values, string orderBy = "", bool isDescending = false)
    {
      // todo: Humanize the values
      
      var q = 
        from e in _entities 
        select e;

      if (!string.IsNullOrWhiteSpace(orderBy) && !isDescending)
      {
        var orderByExpression = GetOrderByExpression(orderBy);
        q = q.OrderBy(orderByExpression);
      }

      if (!string.IsNullOrWhiteSpace(orderBy) && isDescending)
      {
        var orderByExpression = GetOrderByExpression(orderBy);
        q = q.OrderByDescending(orderByExpression);
      }

      var typeFieldNames = 
        from x in typeof(T).GetProperties() 
        select x.Name;
      
      var values0 = values
        .Where(p1 => p1.Key != "order")
        .Where(p2 => p2.Value != null)
        .Where(p3 => p3.Value is string)
        .Where(p4 => !string.IsNullOrWhiteSpace(p4.Value.ToString()))
        .Where(p5 => typeFieldNames.Contains(p5.Key));

      foreach (var pair in values0)
      {
        var field = pair.Key;
        var value = pair.Value;
        var expression = GetExpresionTree(field, value);
        
        q = q.Where(expression);
                
      }
      
      // todo: create where clause for the other non string type fields
      
      // This is the default if no sort is specified the query will sort on the primary key
      if (string.IsNullOrWhiteSpace(orderBy))
      {
        var keys = Keys();
      
        foreach (var key in keys)
        {
          if (typeFieldNames.Contains(key.Name))
          {
            var q2 = CreateSelectorExpression(key.Name, key);

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
            else if (key.PropertyInfo.PropertyType == typeof(long))
            {
              var q3 = (Expression<Func<T, long>>)q2;
      
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
      
      return q.GetPagedData(pager);

    }

    private static LambdaExpression CreateSelectorExpression(string property, IPropertyBase key)
    {
      var r = Expression.Parameter(typeof(T));
      var expression = Expression.Lambda(Expression.PropertyOrField(r, property), r);

      if (key.PropertyInfo.PropertyType == typeof(string))
      {
        return (Expression<Func<T, string>>)expression;
      }

      if (key.PropertyInfo.PropertyType == typeof(byte))
      {
        return (Expression<Func<T, byte>>)expression;
      }

      if (key.PropertyInfo.PropertyType == typeof(short))
      {
        return (Expression<Func<T, short>>)expression;
      }

      if (key.PropertyInfo.PropertyType == typeof(int))
      {
        return (Expression<Func<T, int>>)expression;
      }

      if (key.PropertyInfo.PropertyType == typeof(long))
      {
        return (Expression<Func<T, long>>)expression;
      }

      if (key.PropertyInfo.PropertyType == typeof(DateTime))
      {
        return (Expression<Func<T, DateTime>>)expression;
      }

      return expression;

    }
    
    public PagedResult<T> Find(Pager pager) =>  _entities?.GetPaged(pager);

    public T Find(object id) => _entities.Find(id);
    public bool Exists(object id) => Find(id) != null;

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
    protected bool Delete(object id)
    {
      var entity = _entities.Find(id);

      _entities.Remove(entity);

      SaveChanges();

      return SaveChanges();

    }
    private IEnumerable<IProperty> Keys() => Keys(typeof(T));
    private IEnumerable<IProperty> Keys(Type type) =>  _db.Keys(type);
    
  }
  
}