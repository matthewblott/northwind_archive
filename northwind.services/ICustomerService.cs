namespace northwind.services
{
  using commands;
  using types;
  using models.customers;
  using northwind.domain.models;
  using common.data;

  public interface ICustomerService : IServiceActions<Customer>
  {
    PagedData<CustomerServiceModel> Find(Pager pager, IQueryValues values, string orderBy = "", bool isDescending = false);
    int CreatePartial(CustomerUpdatePartial entity);
    bool UpdatePartial(CustomerUpdatePartial entity);
  }
  
}