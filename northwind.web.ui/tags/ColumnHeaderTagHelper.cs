namespace northwind.web.ui.tags
{
  using Microsoft.AspNetCore.Mvc.Rendering;
  using Microsoft.AspNetCore.Mvc.TagHelpers;
  using Microsoft.AspNetCore.Mvc.ViewFeatures;
  using Microsoft.AspNetCore.Razor.TagHelpers;
  using Microsoft.AspNetCore.Routing;
  using services;
  
  [HtmlTargetElement("col-header")]
  public class ColumnHeaderTagHelper : AnchorTagHelper
  {
    public ColumnHeaderTagHelper(IHtmlGenerator generator) : base(generator) { }

    [HtmlAttributeName("asp-query-parameters")]
    public IQueryParameters QueryParameters { set; get; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      output.TagName = null;
      output.TagMode = TagMode.StartTagAndEndTag;

      var a = A();
      var span = Span();

      a.InnerHtml.AppendHtml(span);
      output.Content.SetHtmlContent(a);
    }

    private TagBuilder A()
    {
      var routeValueDictionary = (RouteValueDictionary) null;

      AddDescIfRequired();

      if (RouteValues != null && RouteValues.Count > 0)
        routeValueDictionary = new RouteValueDictionary(RouteValues);

      var a = Generator.GenerateActionLink(ViewContext, string.Empty, Action, Controller, Protocol,
        Host, Fragment, routeValueDictionary, null);

      return a;
      
    }
    
    private TagBuilder Span()
    {
      var span = new TagBuilder("span");

      span.Attributes.Add("class", "fa-stack fa-1x");

      var upIcon = UpIcon();

      span.InnerHtml.AppendHtml(upIcon);

      var downIcon = DownIcon();

      span.InnerHtml.AppendHtml(downIcon);

      return span;
    }

    private TagBuilder UpIcon()
    {
      var icon = new TagBuilder("i");
      var color = IsSelected() && !IsDescending() ? "white" : "grey-light";
      
      icon.Attributes.Add("class", $"fa fa-sort-up fa-stack-1x has-text-{color}");

      return icon;

    }

    private TagBuilder DownIcon()
    {
      var icon = new TagBuilder("i");
      var color = IsSelected() && IsDescending() ? "white" : "grey-light";
      
      icon.Attributes.Add("class", $"fa fa-sort-down fa-stack-1x has-text-{color}");

      return icon;

    }
    
    private bool IsSelected() => QueryParameters.OrderBy == OrderBy();

    private bool IsDescending() => QueryParameters.IsDescending;
    
    private string OrderBy()
    {
      RouteValues.TryGetValue("order", out var order);

      return order;
    }

    private void AddDescIfRequired()
    {
      if (OrderBy() != QueryParameters.OrderBy)
      {
        return;
      }

      var value = !QueryParameters.IsDescending;
      var desc = value.ToString().ToLower();

      RouteValues.Add("desc", desc);
      
    }
    
  }
  
}