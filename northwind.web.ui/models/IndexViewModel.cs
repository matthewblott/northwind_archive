using System.Collections.Generic;
using northwind.services.types;

namespace northwind.web.ui.models
{
  public class IndexViewModel<T> where T : class
  {
    public IEnumerable<T> Data { get; }
    public IQueryValues QueryValues { get; }
    public Pager Pager { get; }
    public string OrderBy { get; }
    public bool IsDescending { get; }

    public IndexViewModel(IEnumerable<T> data, Pager pager, IQueryValues queryValues, string orderBy, bool isDescending)
    {
      Pager = pager;
      QueryValues = queryValues;
      Data = data;
      OrderBy = orderBy;
      IsDescending = isDescending;
    }
    
  }
  
}