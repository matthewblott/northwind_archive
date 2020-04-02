namespace northwind.web.ui.controllers
{
  using System;
  using System.Collections.Generic;
  using AutoMapper;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.AspNetCore.Mvc.Rendering;
  using northwind.domain.models;
  using common.data;
  using services;
  using services.commands;
  using services.models.customers;
  using services.types;
  using models;
  using models.customers;
  using domain.types;
  
  public class CustomersController : Controller
  {
    private readonly ICustomerService _customerService;
    private readonly IMapper _mapper;
    private readonly IHtmlHelper _html;

    public CustomersController(ICustomerService customerService, IMapper mapper, IHtmlHelper html)
    {
      _customerService = customerService;
      _mapper = mapper;
      _html = html;
    }

    public IActionResult Index(int page, string order, bool desc, string id, string companyName, string region)
    {
      var values = new QueryValues { {nameof(id), id}, {nameof(companyName), companyName}, {nameof(region), region},};
      var result = _customerService.Find(new Pager(page), values, order, desc);
      var data = result.Data;
      var viewModel = new IndexViewModel<CustomerServiceModel>(data, result.Pager, values, order, desc);
      
      return View(viewModel);
      
    }

    public IActionResult New()
    {
      var viewModel = new CustomerViewModel
      {
        Regions = GetRegions()
      };

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

      viewModel.Regions = GetRegions();
      
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
      var model =  _mapper.Map<CustomerUpdatePartial>(viewModel);

      _customerService.CreatePartial(model);

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
      var model =  _mapper.Map<CustomerUpdatePartial>(viewModel);

      _customerService.UpdatePartial(model);
      
      return Redirect(viewModel.ReturnUrl);
      
    }

    [HttpPost]
    public IActionResult Delete(string id)
    {
       _customerService.Delete(id);

      return RedirectToAction(nameof(Index));
      
    }

    public IActionResult IdExists(string id) => Json(_customerService.Exists(id));

    private  IEnumerable<SelectListItem> GetRegions() => _html.GetEnumSelectList<Region>();
    
  }
  
}