using System;
using cloudscribe.Pagination.Models;
using northwind.services;

namespace northwind.web.ui.models
{
  public class CustomersViewModel
  {
    public CustomerQueryParameters Parameters { get; }
    public PagedResult<CustomerPartialViewModel> PagedResult { get; }
    public CustomersViewModel(CustomerQueryParameters parameters, PagedResult<CustomerPartialViewModel> pagedResult)
    {
      Parameters = parameters;
      PagedResult = pagedResult;
    }
    
  }
  
}