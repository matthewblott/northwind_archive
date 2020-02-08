using cloudscribe.Pagination.Models;
using northwind.domain.models;
using northwind.services.commands;
using northwind.services.types;

namespace northwind.services
{
  public interface ICustomerService : IServiceActions<Customer>
  {
    PagedResult<Customer> Find(Pager pager, IQueryValues values, string orderBy = "", bool isDescending = false);
    int CreatePartial(CustomerUpdatePartial entity);
    bool UpdatePartial(CustomerUpdatePartial entity);
  }
  
}