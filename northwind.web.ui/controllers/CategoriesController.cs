using System;
using AutoMapper;
using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Mvc;
using northwind.domain.models;
using northwind.services;
using northwind.web.ui.models;
using northwind.services.types;

namespace northwind.web.ui.controllers
{
  public class CategoriesController : Controller
  {
    private readonly ICategoryService _service;
    private readonly IMapper _mapper;
    
    public CategoriesController(ICategoryService service, IMapper mapper)
    {
      _service = service;
      _mapper = mapper;
    }

    public IActionResult Index(long page, string order, bool desc, long id, string name, string description)
    {
      var parameters = new CategoryQueryParameters
      {
        Id = id, OrderBy = order, IsDescending = desc, CategoryName = name, Description = description
      };
      
      var result = _service.Find(new Pager(page), parameters);
      var result0 =  _mapper.Map<PagedResult<CategoryViewModel>>(result);
      var model = new CategoriesViewModel(parameters, result0);

      return View(model);
      
    }

    public IActionResult New()
    {
      var model =  new CategoryViewModel();
      
      return View(model);
      
    }

    public IActionResult Show(long id)
    {
      var result = _service.Find(id);
      var model =  _mapper.Map<CategoryViewModel>(result);
      
      return View(model);
      
    }

    public IActionResult Edit(long id)
    {
      var result = _service.Find(id);
      var model =  _mapper.Map<CategoryViewModel>(result);
      
      return View(model);
      
    }

    public IActionResult Create(CategoryViewModel viewModel)
    {
      var model =  _mapper.Map<Category>(viewModel);
      var result = _service.Create(model);
      var id = model.Id;
      
      return RedirectToAction(nameof(Show), new { id });
      
    }

    public IActionResult Update(CategoryViewModel viewModel)
    {
      var model =  _mapper.Map<Category>(viewModel);
      var id = viewModel.Id;
      
      _service.Update(model);

      return RedirectToAction(nameof(Show), new { id });
      
    }

    public IActionResult Delete(long id)
    {
      _service.Delete(id);

      return RedirectToAction(nameof(Index));
      
    }
    
  }
  
}