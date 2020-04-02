namespace northwind.tests.xunit.controllers
{
  using Xunit;
  using MyTested.AspNetCore.Mvc;
  using web;

  public class HomeControllerTests
  {
    [Fact]
    public void Foo()
    {
      var ctl =  MyController<HomeController>.Instance();

      var func = 
        ctl.Calling(c => c.Index());

      func.ShouldReturn().Ok();

      // .Calling(c => c.Index())
      // .ShouldReturn().View();

    }
    
  }
  
}