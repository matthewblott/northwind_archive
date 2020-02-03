using northwind.domain;
using northwind.domain.models;

namespace northwind.services
{
  public class CategoryService : ServiceBase<Category>, ICategoryService
  {
    public CategoryService(IContext db) : base(db, db.Categories)
    {
    }

    public new int Create(Category entity)
    {
      var retVal = base.Create(entity);
     
      _db.Commit();
    
      return retVal;
      
    }

    public new bool Update(Category entity)
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