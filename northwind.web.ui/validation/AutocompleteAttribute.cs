using System;

namespace northwind.web.ui.validation
{
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
  public class AutocompleteAttribute : Microsoft.AspNetCore.Mvc.RemoteAttribute
  {
    public AutocompleteAttribute(string action, string controller) : base(action, controller) { }
  }
}