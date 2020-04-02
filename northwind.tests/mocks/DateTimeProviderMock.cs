namespace northwind.tests.mocks
{
  using System;
  using services;
  using FakeItEasy;
  
  public class DateTimeProviderMock
  {
    public static IDateTimeProvider Instance
    {
      get
      {
        var provider = A.Fake<IDateTimeProvider>();

        A.CallTo(() => provider.Now()).Returns(new DateTime(2020,2, 1));
        
        return provider;
      }
    }
    
  }
}