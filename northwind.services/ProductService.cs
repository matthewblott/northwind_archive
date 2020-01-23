using LazyCache;
using northwind.domain;
using northwind.domain.models;

namespace northwind.services
{
  public class ProductService : ServiceBase<Product>, IProductService
  {
    private readonly IAppCache _cache;
    
    public ProductService(IContext db, IAppCache cache) : base(db, db.Products)
    {
      _cache = cache;
    }
    
    public new int Create(Product entity)
    {
      var retVal = base.Create(entity);
     
      _db.Commit();
    
      return retVal;
    
    }
    
    public new bool Update(Product entity)
    {
      var retVal = base.Update(entity);
      
      _db.Commit();
    
      return retVal;
    
    }
    
    public new bool Delete(params object[] keyValues)
    {
      var retVal = base.Delete(keyValues);
      
      _db.Commit();
    
      return retVal;
    
    }
    
  }
  
}