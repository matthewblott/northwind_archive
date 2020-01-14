namespace northwind.services
{
  public interface IQueryParameters
  {
    string OrderBy { get; set; }
    bool IsDescending { get; set; }
  }
}