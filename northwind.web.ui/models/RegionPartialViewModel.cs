using AutoMapper;

namespace northwind.web.ui.models
{
  public class RegionPartialViewModel
  {
    private readonly IMapper _mapper;
    public int Id { get; set; }
    public string RegionDescription { get; set; }
  }
}