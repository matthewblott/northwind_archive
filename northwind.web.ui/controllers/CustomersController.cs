using System;
using AutoMapper;
using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Mvc;
using northwind.domain.models;
using northwind.services;
using northwind.services.types;
using northwind.web.ui.models;

namespace northwind.web.ui.controllers
{
  public class CustomersController : Controller
  {
    private readonly ICustomerService _service;
    private readonly IMapper _mapper;
    
    public CustomersController(ICustomerService service, IMapper mapper)
    {
      _service = service;
      _mapper = mapper;
    }

    public IActionResult Index(long page, string id, string name, string order, bool desc)
    {
      var parameters = new CustomerQueryParameters
      {
        Id = id, OrderBy = order, IsDescending = desc, CompanyName = name
      };
      
      var result = _service.Find(new Page(page), parameters);
      var result0 =  _mapper.Map<PagedResult<PartialCustomerViewModel>>(result);
      var model = new CustomersViewModel(parameters, result0);

      return View(model);
      
    }

    public IActionResult New()
    {
      var model =  new CustomerViewModel();
      
      return View(model);
      
    }

    public IActionResult Show(string id)
    {
      var result = _service.Find(id.ToUpper());
      var model =  _mapper.Map<CustomerViewModel>(result);
      
      return View(model);
      
    }

    public IActionResult Edit(string id)
    {
      var result = _service.Find(id.ToUpper());
      var model =  _mapper.Map<CustomerViewModel>(result);
      
      return View(model);
      
    }

    [HttpPost]
    public IActionResult Create(CustomerViewModel viewModel)
    {
      var model =  _mapper.Map<Customer>(viewModel);
      var result = _service.Create(model);
      var id = model.Id.ToUpper();
      
      if (result == 0)
      {
        return RedirectToAction(nameof(Show), new { id });
      }
      
      throw new Exception();
      
    }
    
    [HttpPost]
    public IActionResult CreatePartial(UpdatePartialCustomerViewModel viewModel)
    {
      var model = new Customer
      {
        Id = viewModel.Id, CompanyName = viewModel.CompanyName, Region = viewModel.Region
      };

      _service.Create(model);

      return Redirect(viewModel.ReturnUrl);

    }

    [HttpPost]
    public IActionResult Update(CustomerViewModel viewModel)
    {
      var model =  _mapper.Map<Customer>(viewModel);
      var id = viewModel.Id.ToUpper();
      var result = _service.Update(model);

      return RedirectToAction(nameof(Show), new { id });
      
    }
    [HttpPost]
    public IActionResult UpdatePartial(UpdatePartialCustomerViewModel viewModel)
    {
      var model = _service.Find(viewModel.Id);

      model.CompanyName = viewModel.CompanyName;
      model.Region = viewModel.Region;
      
      _service.Update(model);
      
      return Redirect(viewModel.ReturnUrl);
      
    }

    [HttpPost]
    public IActionResult Delete(string id)
    {
      var result = _service.Delete(id);

      return RedirectToAction(nameof(Index));
      
      }
    
  }
  
}