using northwind.domain;
using northwind.domain.models;
using northwind.services.commands;

namespace northwind.services
{
  public class CustomerService : ServiceBase<Customer>, ICustomerService
  {
    public CustomerService(IContext db) : base(db, db.Customers)
    {
    }
    
    public new int Create(Customer entity)
    {
      var retVal = base.Create(entity);
     
      _db.Commit();
    
      return retVal;
    
    }

    public bool UpdatePartial(CustomerUpdatePartial entity)
    {
      var currentEntity = Find(entity.Id);

      currentEntity.CompanyName = entity.CompanyName;
      currentEntity.Region = entity.Region;
      
      var retVal = base.Update(currentEntity);
      
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