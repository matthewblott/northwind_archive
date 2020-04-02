namespace northwind.tests.controllers
{
  using NUnit.Framework;
  using MyTested.AspNetCore.Mvc;
  using northwind.web.ui.controllers;
  
  public class HomeControllerTests
  {
    [Test]
    public void Foo()
    {
      MyController<HomeController>.Instance()
        .Calling(c => c.Index())
        .ShouldReturn().View();
    }
    
  }
  
}