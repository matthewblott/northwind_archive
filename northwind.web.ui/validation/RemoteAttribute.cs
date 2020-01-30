using System;

namespace northwind.web.ui.validation
{
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
  public class RemoteAttribute : Microsoft.AspNetCore.Mvc.RemoteAttribute
  {
    public string Controller => RouteData["controller"].ToString();
    public string Action => RouteData["action"].ToString();
    public RemoteAttribute(string action, string controller) : base(action, controller) { }
  }
}