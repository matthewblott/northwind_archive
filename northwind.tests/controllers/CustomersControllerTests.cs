namespace northwind.tests.controllers
{
  using NUnit;
  using NUnit.Framework;
  using MyTested.AspNetCore.Mvc;
  using northwind.web.ui.controllers;
  
  public class CustomersControllerTests
  {
    [Test]
    public void Foo()
    {
      int page = 1;
      string order = "";
      bool desc = false;
      string id = "";
      string companyName = "";
      string region = "";

      MyController<CustomersController>.Instance()
        .Calling(c => c.Index(page, order, desc, id, companyName, region))
        .ShouldReturn().View();
    }
    
  }
  
}