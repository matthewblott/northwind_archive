namespace northwind.services.implementations
{
  using System.Collections.Generic;
  using AutoMapper;
  using common.data;
  using domain;
  using northwind.domain.models;
  using models.territories;
  using types;

  public class TerritoryService : ServiceBase<Territory>, ITerritoryService
  {
    private readonly IMapper _mapper;
    
    public TerritoryService(Context db, IMapper mapper) : base(db, db.Territories)
    {
      _mapper = mapper;
    }
    
    public new PagedData<TerritoryServiceModel> Find(
      Pager pager, IQueryValues values, string orderBy = "", bool isDescending = false)
    {
      var result = base.Find(pager, values, orderBy, isDescending);
      var data = _mapper.Map<IEnumerable<TerritoryServiceModel>>(result.Data);
      var pagedData = new PagedData<TerritoryServiceModel>(data, result.Pager);

      return pagedData;

    }

    public new int Create(Territory entity)
    {
      var retVal = base.Create(entity);
     
      _db.Commit();
    
      return retVal;
      
    }

    public new bool Update(Territory entity)
    {
      var retVal = base.Update(entity);
      
      _db.Commit();
    
      return retVal;
    }

    public new bool Delete(object id)
    {
      var retVal = base.Delete(id);
      
      _db.Commit();
    
      return retVal;
      
    }

  }
  
}