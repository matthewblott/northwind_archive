namespace northwind.services.implementations
{
  using domain;
  using northwind.domain.models;
  public class RegionService : ServiceBase<Region>, IRegionService
  {
    public RegionService(IContext db) : base(db, db.Regions)
    {
      
    }
    
    public new int Create(Region entity)
    {
      var retVal = base.Create(entity);
     
      _db.Commit();
    
      return retVal;
      
    }

    public new bool Update(Region entity)
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