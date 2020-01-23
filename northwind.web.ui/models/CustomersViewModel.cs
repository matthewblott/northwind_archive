using System;
using cloudscribe.Pagination.Models;
using northwind.services;

namespace northwind.web.ui.models
{
  public class CustomersViewModel
  {
    public CustomerQueryParameters Parameters { get; }
    public PagedResult<PartialCustomerViewModel> PagedResult { get; }
    public CustomersViewModel(CustomerQueryParameters parameters, PagedResult<PartialCustomerViewModel> pagedResult)
    {
      Parameters = parameters;
      PagedResult = pagedResult;
    }
    
  }
  
}