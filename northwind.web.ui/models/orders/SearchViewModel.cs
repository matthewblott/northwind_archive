namespace northwind.web.ui.models.orders
{
  using System;
  using System.ComponentModel.DataAnnotations;
  using common;
  using common.mapping;
  using northwind.services.models.orders;
  
  public class SearchViewModel : IMapFrom<SearchServiceModel>, IMapTo<SearchServiceModel>
  {
    public int EmployeeId { get; set; }
    public string CustomerId { get; set; }
    
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = DateFormats.InternationalDate)]
    public DateTime? OrderDateFrom { get; set; }
    
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = DateFormats.InternationalDate)]
    public DateTime? OrderDateTo { get; set; }
  }
}