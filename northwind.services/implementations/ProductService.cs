namespace northwind.services.implementations
{
  using System.Collections.Generic;
  using AutoMapper;
  using domain;
  using northwind.domain.models;
  using common.data;
  using models.products;
  using types;
  
  public class ProductService : ServiceBase<Product>, IProductService
  {
    private readonly IMapper _mapper;
    
    public ProductService(IContext db, IMapper mapper) : base(db, db.Products)
    {
      _mapper = mapper;
    }
    
    public new PagedData<ProductServiceModel> Find(Pager pager, IQueryValues values, string orderBy = "", bool isDescending = false)
    {
      var result = base.Find(pager, values, orderBy, isDescending);
      var data = _mapper.Map<IEnumerable<ProductServiceModel>>(result.Data);
      var pagedData = new PagedData<ProductServiceModel>(data, result.Pager);

      return pagedData;

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