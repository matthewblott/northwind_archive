using System;
using cloudscribe.Pagination.Models;
using northwind.services;

namespace northwind.web.ui.models
{
  public class CustomersViewModel
  {
    public CustomerQueryParameters Parameters { get; }
    public PagedResult<CustomerViewModel> PagedResult { get; }
    public CustomersViewModel(CustomerQueryParameters parameters, PagedResult<CustomerViewModel> pagedResult)
    {
      Parameters = parameters;
      PagedResult = pagedResult;
    }
    
  }
  
}