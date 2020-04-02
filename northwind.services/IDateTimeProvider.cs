namespace northwind.services
{
  using System;
  
  public interface IDateTimeProvider
  {
    DateTime Now();
  }
}