using System;
using AutoMapper;
using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using northwind.domain.models;
using northwind.services;
using northwind.services.types;
using northwind.web.ui.models;

namespace northwind.web.ui.controllers
{
  public class CustomersController : Controller
  {
    private readonly ICustomerService _customerService;
    private readonly IRegionService _regionService;
    private readonly IMapper _mapper;
    
    public CustomersController(ICustomerService customerService, IMapper mapper, IRegionService regionService)
    {
      _customerService = customerService;
      _mapper = mapper;
      _regionService = regionService;
    }

    public IActionResult Index(long page, string order, bool desc, string id, string name)
    {
      var parameters = new CustomerQueryParameters
      {
        Id = id, OrderBy = order, IsDescending = desc, CompanyName = name
      };
      
      var result = _customerService.Find(new Page(page), parameters);
      var result0 =  _mapper.Map<PagedResult<CustomerPartialViewModel>>(result);
      var model = new CustomersViewModel(parameters, result0);

      return View(model);
      
    }

    public IActionResult New()
    {
      var viewModel =  new CustomerViewModel();
      
      return View(viewModel);
      
    }

    public IActionResult Show(string id)
    {
      var model = _customerService.Find(id.ToUpper());
      var viewModel =  _mapper.Map<CustomerViewModel>(model);
      
      viewModel.Regions = GetRegions();
      
      return View(viewModel);
      
    }

    public IActionResult Edit(string id)
    {
      var model = _customerService.Find(id.ToUpper());
      var viewModel =  _mapper.Map<CustomerViewModel>(model);
      
      return View(viewModel);
      
    }

    [HttpPost]
    public IActionResult Create(CustomerViewModel viewModel)
    {
      var model =  _mapper.Map<Customer>(viewModel);
      var result = _customerService.Create(model);
      var id = model.Id.ToUpper();
      
      if (result == 0)
      {
        return RedirectToAction(nameof(Show), new { id });
      }
      
      throw new Exception();
      
    }
    
    [HttpPost]
    public IActionResult CreatePartial(CustomerUpdatePartialViewModel viewModel)
    {
      var model = new Customer
      {
        Id = viewModel.Id.ToUpper(), CompanyName = viewModel.CompanyName, Region = viewModel.Region
      };

      _customerService.Create(model);

      return Redirect(viewModel.ReturnUrl);

    }

    [HttpPost]
    public IActionResult Update(CustomerViewModel viewModel)
    {
      var model =  _mapper.Map<Customer>(viewModel);
      var id = viewModel.Id.ToUpper();
      
      _customerService.Update(model);

      return RedirectToAction(nameof(Show), new { id });
      
    }
    
    [HttpPost]
    public IActionResult UpdatePartial(CustomerUpdatePartialViewModel viewModel)
    {
      var model = _customerService.Find(viewModel.Id);

      model.CompanyName = viewModel.CompanyName;
      model.Region = viewModel.Region;
      
      _customerService.Update(model);
      
      return Redirect(viewModel.ReturnUrl);
      
    }

    [HttpPost]
    public IActionResult Delete(string id)
    {
       _customerService.Delete(id);

      return RedirectToAction(nameof(Index));
      
    }

    public IActionResult IdExists(string id) => Json(_customerService.Exists(id));
   
    private SelectList GetRegions()
    {
      var regions = _regionService.Find(new Page(1));
      var items = regions.Data;
      var selectList = new SelectList(items, nameof(Region.RegionDescription),  nameof(Region.RegionDescription));
    
      return selectList;
    
    }
    
  }
  
}