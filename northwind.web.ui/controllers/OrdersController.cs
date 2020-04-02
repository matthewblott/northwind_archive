namespace northwind.web.ui.controllers
{
  using System.Linq;
  using models.orders;
  using Microsoft.AspNetCore.Mvc;
  using AutoMapper;
  using common.data;
  using services;
  using northwind.services.models.orders;
  using services.types;
  using models;
  
  public class OrdersController : Controller
  {
    private readonly IOrderService _orderService;
    private readonly ISqlRawService _sqlRawService;
    private readonly IMapper _mapper;

    public OrdersController(IOrderService orderService, IMapper mapper, ISqlRawService sqlRawService)
    {
      _orderService = orderService;
      _mapper = mapper;
      _sqlRawService = sqlRawService;
    }

    public IActionResult Index(int page, string order, bool desc, int id, string name)
    {
      var values = new QueryValues { {nameof(id), id}, {nameof(name), name} };
      var result = _orderService.Find(new Pager(page), values, order, desc);
      var data = result.Data;
      var viewModel = new IndexViewModel<OrderServiceModel>(data, result.Pager, values, order, desc);
      
      return View(viewModel);
      
    }

    public IActionResult Search() => View();

    public IActionResult SearchResults(SearchViewModel viewModel)
    {
      var model = _mapper.Map<SearchServiceModel>(viewModel);
      var data = _orderService.Find(model);
      var newData =  data.Take(10);
      var newViewModel = _mapper.Map<SearchResultsViewModel>(viewModel);

      newViewModel.Data = newData;
      
      return View(newViewModel);

    }

    public IActionResult Recent(string customerId = "HILAA")
    {
      var model = _sqlRawService.FindRecentOrders(customerId);
      
      return View(model);
    }
    
  }
  
}