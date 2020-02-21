namespace northwind.common.data
{
  using System.Collections.Generic;

  public class PagedData<T>
  {
    public IEnumerable<T> Data { get; }  
    public Pager Pager { get; }

    public PagedData(IEnumerable<T> data, Pager pager)
    {
      Data = data;
      Pager = pager;
    }
    
  }
  
}