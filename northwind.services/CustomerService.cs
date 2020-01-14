using LazyCache;
using northwind.domain;
using northwind.domain.models;

namespace northwind.services
{
  public class CustomerService : ServiceBase<Customer>, ICustomerService
  {
    private readonly IAppCache _cache;
    
    public CustomerService(IContext db, IAppCache cache) : base(db, db.Customers)
    {
      _cache = cache;
    }
    
    public new int Create(Customer entity)
    {
      var retVal = base.Create(entity);
     
      _db.Commit();
    
      return retVal;
    
    }
    
    public new bool Update(Customer entity)
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