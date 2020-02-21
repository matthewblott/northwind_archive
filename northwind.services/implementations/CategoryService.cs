namespace northwind.services.implementations
{
  using System.Collections.Generic;
  using AutoMapper;
  using common.data;
  using domain;
  using domain.models;
  using models.categories;
  using types;
  
  public class CategoryService : ServiceBase<Category>, ICategoryService
  {
    private readonly IMapper _mapper;
    public CategoryService(IContext db, IMapper mapper) : base(db, db.Categories)
    {
      _mapper = mapper;
    }

    public new PagedData<CategoryServiceModel> Find(
      Pager pager, IQueryValues values, string orderBy = "", bool isDescending = false)
    {
      var result = base.Find(pager, values, orderBy, isDescending);
      var data = _mapper.Map<IEnumerable<CategoryServiceModel>>(result.Data);
      var pagedData = new PagedData<CategoryServiceModel>(data, result.Pager);

      return pagedData;

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