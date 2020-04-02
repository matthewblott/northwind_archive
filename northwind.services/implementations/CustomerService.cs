using northwind.common.data;

namespace northwind.services.implementations
{
  using System.Collections.Generic;
  using AutoMapper;
  using cloudscribe.Pagination.Models;
  using commands;
  using domain;
  using models.customers;
  using northwind.domain.models;
  using types;

  public class CustomerService : ServiceBase<Customer>, ICustomerService
  {
    private readonly IMapper _mapper;
    public CustomerService(Context db, IMapper mapper) : base(db, db.Customers)
    {
      _mapper = mapper;
    }
    
    public new int Create(Customer entity)
    {
      var retVal = base.Create(entity);
     
      _db.Commit();
    
      return retVal;
    
    }

    public new PagedData<CustomerServiceModel> Find(
      Pager pager, IQueryValues values, string orderBy = "", bool isDescending = false)
    {
      var result = base.Find(pager, values, orderBy, isDescending);
      var data = _mapper.Map<IEnumerable<CustomerServiceModel>>(result.Data);
      var pagedData = new PagedData<CustomerServiceModel>(data, result.Pager);

      return pagedData;

    }

    public int CreatePartial(CustomerUpdatePartial entity)
    {
      var currentEntity = Find(entity.Id);

      currentEntity.CompanyName = entity.CompanyName;
      currentEntity.Region = entity.Region;
      
      var retVal = base.Create(currentEntity);
      
      _db.Commit();
    
      return retVal;

    }

    public new bool Update(Customer entity)
    {
      var retVal = base.Update(entity);
      
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
    
    public new bool Delete(object id)
    {
      var retVal = base.Delete(id);
      
      _db.Commit();
    
      return retVal;
    
    }
    
  }
  
}