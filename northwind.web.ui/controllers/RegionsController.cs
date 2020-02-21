namespace northwind.web.ui.controllers
{
  using AutoMapper;
  using cloudscribe.Pagination.Models;
  using Microsoft.AspNetCore.Mvc;
  using common.data;
  using services;
  using models;
  using northwind.domain.models;

  public class RegionsController : Controller
  {
    private readonly IRegionService _service;
    private readonly IMapper _mapper;
    
    public RegionsController(IRegionService service, IMapper mapper)
    {
      _service = service;
      _mapper = mapper;
    }

    // public IActionResult Index()
    // {
    //   var parameters = new RegionQueryParameters();
    //   
    //   var result = _service.Find(new Pager(1));
    //   var mappedResult =  _mapper.Map<PagedResult<RegionPartialViewModel>>(result);
    //   var viewModel = new RegionsViewModel(parameters, mappedResult);
    //
    //   return View(viewModel);
    // }
    //
    // public IActionResult New()
    // {
    //   var model =  new RegionViewModel();
    //   
    //   return View(model);
    //   
    // }
    //
    // public IActionResult Show(long id)
    // {
    //   var result = _service.Find(id);
    //   var model =  _mapper.Map<RegionViewModel>(result);
    //   
    //   return View(model);
    //   
    // }
    //
    // public IActionResult Edit(long id)
    // {
    //   var result = _service.Find(id);
    //   var model =  _mapper.Map<RegionViewModel>(result);
    //   
    //   return View(model);
    //   
    // }
    //
    // public IActionResult Create(RegionViewModel viewModel)
    // {
    //   var model =  _mapper.Map<Region>(viewModel);
    //   var id = model.Id;
    //
    //   _service.Create(model);
    //   
    //   return RedirectToAction(nameof(Show), new { id });
    //   
    // }
    //
    // public IActionResult Update(RegionViewModel viewModel)
    // {
    //   var model =  _mapper.Map<Region>(viewModel);
    //   var id = viewModel.Id;
    //   
    //   _service.Update(model);
    //
    //   return RedirectToAction(nameof(Show), new { id });
    //   
    // }
    //
    // public IActionResult Delete(long id)
    // {
    //   _service.Delete(id);
    //
    //   return RedirectToAction(nameof(Index));
    //   
    // }

  }
  
}