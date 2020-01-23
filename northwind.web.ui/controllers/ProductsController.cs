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
  public class ProductsController : Controller
  {
    private readonly IProductService _service;
    private readonly IMapper _mapper;
    
    public ProductsController(IProductService service, IMapper mapper)
    {
      _service = service;
      _mapper = mapper;
    }

    public IActionResult Index(long page, long id, string name, string order, bool desc)
    {
      var parameters = new ProductQueryParameters
      {
        Id = id, OrderBy = order, IsDescending = desc, ProductName = name
      };
      
      var result = _service.Find(new Page(page), parameters);
      var result0 =  _mapper.Map<PagedResult<ProductViewModel>>(result);
      var model = new ProductsViewModel(parameters, result0);

      return View(model);
      
    }

    public IActionResult New()
    {
      var model =  new ProductViewModel();
      
      return View(model);
      
    }

    public IActionResult Show(long id)
    {
      var result = _service.Find(id);
      var model =  _mapper.Map<ProductViewModel>(result);
      
      return View(model);
      
    }

    public IActionResult Edit(long id)
    {
      var result = _service.Find(id);
      var model =  _mapper.Map<ProductViewModel>(result);
      
      return View(model);
      
    }

    public IActionResult Create(ProductViewModel viewModel)
    {
      var model =  _mapper.Map<Product>(viewModel);
      var result = _service.Create(model);
      var id = model.Id;
      
      if (result == 0)
      {
        return RedirectToAction(nameof(Show), new { id });
      }
      
      throw new Exception();
      
    }

    public IActionResult Update(ProductViewModel viewModel)
    {
      var model =  _mapper.Map<Product>(viewModel);
      var id = viewModel.Id;
      var result = _service.Update(model);

      if (result)
      {
        return RedirectToAction(nameof(Show), new { id });
      }
      
      throw new Exception();
      
    }

    public IActionResult Delete(long id)
    {
      var result = _service.Delete(id);

      if (result)
      {
        return RedirectToAction(nameof(Index));
      }
      
      throw new Exception();
      
    }
    
  }
  
}