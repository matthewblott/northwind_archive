using AutoMapper;

namespace northwind.web.ui.models
{
  public class CustomerPartialViewModel
  {
    private readonly IMapper _mapper;
    public string Id { get; set; }
    public string CompanyName { get; set; }
    public string Region { get; set; }

  }
  
}