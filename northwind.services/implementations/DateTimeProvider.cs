namespace northwind.services.implementations
{
  using System;
  
  public class DateTimeProvider : IDateTimeProvider
  {
    public DateTime Now() => DateTime.UtcNow;
  }
}