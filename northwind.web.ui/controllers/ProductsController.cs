using northwind.services.models.customers;
using northwind.services.models.products;
using northwind.services.types;

namespace northwind.web.ui.controllers
{
  using System;
  using AutoMapper;
  using cloudscribe.Pagination.Models;
  using Microsoft.AspNetCore.Mvc;
  using northwind.domain.models;
  using services;
  using common.data;
  using models;
  
  public class ProductsController : Controller
  {
    private readonly IProductService _service;
    private readonly IMapper _mapper;
    
    public ProductsController(IProductService service, IMapper mapper)
    {
      _service = service;
      _mapper = mapper;
    }

    public IActionResult Index(long page, string order, bool desc, long id, string name)
    {
      var values = new QueryValues { {nameof(id), id}, {nameof(name), name} };
      var result = _service.Find(new Pager(page), values, order, desc);
      var data = result.Data;
      var viewModel = new IndexViewModel<ProductServiceModel>(data, result.Pager, values, order, desc);
      
      return View(viewModel);
      
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