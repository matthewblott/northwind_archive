namespace northwind.web.ui.controllers
{
  using AutoMapper;
  using Microsoft.AspNetCore.Mvc;
  using northwind.domain.models;
  using northwind.services.models.categories;
  using common.data;
  using services;
  using models;
  using services.types;
  
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
      var values = new QueryValues { {nameof(id), id}, {nameof(name), name}, {nameof(description), description},};
      var result = _service.Find(new Pager(page), values, order, desc);
      var data = result.Data;
      var viewModel = new IndexViewModel<CategoryServiceModel>(data, result.Pager, values, order, desc);
      
      return View(viewModel);
      
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