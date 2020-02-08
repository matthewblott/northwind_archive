using AutoMapper;
using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Mvc;
using northwind.services;
using northwind.web.ui.models;

namespace northwind.web.ui.controllers
{
  public class RegionsController : Controller
  {
    private readonly IRegionService _regionService;
    private readonly IMapper _mapper;
    
    public RegionsController(IRegionService regionService, IMapper mapper)
    {
      _regionService = regionService;
      _mapper = mapper;
    }

    public IActionResult Index()
    {
      var parameters = new RegionQueryParameters();
      
      var result = _regionService.Find(new services.types.Pager(1));
      var mappedResult =  _mapper.Map<PagedResult<RegionPartialViewModel>>(result);
      var viewModel = new RegionsViewModel(parameters, mappedResult);

      return View(viewModel);
    }

    public IActionResult Delete()
    {
      throw new System.NotImplementedException();
    }
  }
  
}