namespace northwind.reporting
{
  public interface IReportService
  {
    string CreateReport(string report, object model);
  }
}