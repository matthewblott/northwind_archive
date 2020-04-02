namespace northwind.web.ui.controllers
{
  using Microsoft.AspNetCore.Mvc;
  using AutoMapper;
  using northwind.domain.models;
  using common.data;
  using services;
  using models;
  using services.types;
  using northwind.services.models.territories;
  using models.territories;

  public class TerritoriesController : Controller
  {
    private readonly ITerritoryService _service;
    private readonly IMapper _mapper;
    
    public TerritoriesController(ITerritoryService service, IMapper mapper)
    {
      _service = service;
      _mapper = mapper;
    }

    public IActionResult Index(int page, string order, bool desc, string id, string name, string description)
    {
      var values = new QueryValues { {nameof(id), id}, {nameof(name), name}, {nameof(description), description},};
      var result = _service.Find(new Pager(page), values, order, desc);
      var data = result.Data;
      var viewModel = new IndexViewModel<TerritoryServiceModel>(data, result.Pager, values, order, desc);
      
      return View(viewModel);
      
    }

    public IActionResult New()
    {
      var model =  new TerritoryViewModel();
      
      return View(model);
      
    }

    public IActionResult Show(string id)
    {
      var result = _service.Find(id);
      var model =  _mapper.Map<TerritoryViewModel>(result);
      
      return View(model);
      
    }

    public IActionResult Edit(string id)
    {
      var result = _service.Find(id);
      var model =  _mapper.Map<TerritoryViewModel>(result);
      
      return View(model);
      
    }

    public IActionResult Create(TerritoryViewModel viewModel)
    {
      var model =  _mapper.Map<Territory>(viewModel);
      var id = model.Id;
      
      _service.Create(model);
      
      return RedirectToAction(nameof(Show), new { id });
      
    }

    public IActionResult Update(TerritoryViewModel viewModel)
    {
      var model =  _mapper.Map<Territory>(viewModel);
      var id = viewModel.Id;
      
      _service.Update(model);

      return RedirectToAction(nameof(Show), new { id });
      
    }

    public IActionResult Delete(string id)
    {
      _service.Delete(id);

      return RedirectToAction(nameof(Index));
      
    }
    
  }
  
}