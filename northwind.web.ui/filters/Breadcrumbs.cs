using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cloudscribe.Web.Navigation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace northwind.web.ui.filters
{
  public class Breadcrumbs : IActionFilter
  {
    private readonly NavigationTreeBuilderService _builderService;
    private readonly IUrlHelperFactory _factory;

    public Breadcrumbs(IServiceProvider services, NavigationTreeBuilderService builderService)
    {
      _builderService = builderService;
      _factory = services.GetRequiredService<IUrlHelperFactory>();
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
      if (!(context.Controller is Controller))
      {
        return;
      }

      var currentNode = GetCurrentNode(context);
      var nodes = new List<NavigationNode>();

      while (currentNode != null)
      {
        nodes.Add(currentNode.Value);
        currentNode = currentNode.Parent;
      }

      var q = 
        from x in nodes 
        where x.Action != "Index" && x.Action != "New"
        select x;

      var helper = _factory.GetUrlHelper(context);
      var keys = GetKeys(context.ActionArguments);

      foreach (var node in q)
      {
        var parameters = node.PreservedRouteParameters.Split(',', StringSplitOptions.RemoveEmptyEntries);
        
        var q0 =
          from x in parameters 
          where keys.Select(y => y.Key).Contains(x)
          select x;
        
        var values = new StringBuilder("?");

        foreach (var p in q0)
        {
          values.Append($"{p}={keys.Single(x => x.Key == p).Value}&");
        }

        var url = helper.Action(node.Action,  node.Controller, new { }) + values;
        var text = keys.Single(x => x.Key == parameters.Last()).Value;

        context.HttpContext.AdjustBreadcrumb(node.Key, text, url);

      }

    }

    private TreeNode<NavigationNode> GetCurrentNode(ActionExecutingContext context)
    {
      var result = _builderService.GetTree();
      var tree = result.Result;
      var controller = context.Controller as ControllerBase;
      var controllerName = controller?.ControllerContext.ActionDescriptor.ControllerName;
      var actionName = controller?.ControllerContext.ActionDescriptor.ActionName;
      var node = tree.FindByKey($"{controllerName}.{actionName}");

      return node;
      
    }
    
    private Dictionary<string, string> GetKeys(IDictionary<string, object> args)
    {
      var id = args.GetValueFromKey<string>("id");

      var keys = new Dictionary<string, string>
      {
        {nameof(id), id},
      };

      return keys;
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
        
  }
  
}