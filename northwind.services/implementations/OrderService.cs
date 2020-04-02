namespace northwind.services.implementations
{
  using System.Globalization;
  using System.IO;
  using CsvHelper;  
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using AutoMapper;
  using common;
  using common.data;
  using domain;
  using models.orders;
  using northwind.domain.models;
  using types;
  
  public class OrderService : ServiceBase<Order>, IOrderService
  {
    private readonly IMapper _mapper;
    
    public OrderService(Context db, IMapper mapper) : base(db, db.Orders)
    {
      _mapper = mapper;
    }
    
    public new PagedData<OrderServiceModel> Find(Pager pager, IQueryValues values, 
      string orderBy = "", bool isDescending = false)
    {
      var result = base.Find(pager, values, orderBy, isDescending);
      var data = _mapper.Map<IEnumerable<OrderServiceModel>>(result.Data);
      var pagedData = new PagedData<OrderServiceModel>(data, result.Pager);

      return pagedData;

    }

    public IEnumerable<OrderServiceModel> Find(SearchServiceModel model)
    {
      var customerId = model.CustomerId;
      var employeeId = model.EmployeeId;
      var orderDateFrom = model.OrderDateFrom ?? new DateTime();
      var orderDateTo = model.OrderDateTo ?? new DateTime();
      
      var q =
        from o in _db.Orders
        select o;

      // if (string.IsNullOrWhiteSpace(customerId) && employeeId == default && orderDateFrom.IsMinValue() &&
      //     orderDateTo.IsMinValue())
      // {
      //   throw new Exception("You must provide some criteria for the query");
      // }
      //
      if (!string.IsNullOrWhiteSpace(customerId))
      {
        q = q.Where(o => o.CustomerId == customerId);
      }

      if (employeeId != default)
      {
        q = q.Where(o => o.EmployeeId == employeeId);
      }

      if (!orderDateFrom.IsMinValue())
      {
        q = q.Where(o => o.OrderDate >= orderDateFrom);
      }
      
      if (!orderDateTo.IsMinValue())
      {
        q = q.Where(o => o.OrderDate <= orderDateTo);
      }

      var list = q.ToList();
      var data = _mapper.Map<IEnumerable<OrderServiceModel>>(list);
      var currentDir = Directory.GetCurrentDirectory();
      var path = Path.Combine(currentDir, "file.csv");
      
      using (var writer = new StreamWriter(path))
      {
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
        csv.WriteRecords(data);
      }

      return data;
      
    }
    
    public new int Create(Order entity)
    {
      var retVal = base.Create(entity);
     
      _db.Commit();
    
      return retVal;
    
    }
    
    public new bool Update(Order entity)
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