using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Humanizer;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using northwind.services;

namespace northwind.web.ui.tags
{
  [HtmlTargetElement("th")]
  public class TableHeaderTagHelper : AnchorTagHelper
  {
    [HtmlAttributeName("nw-column")]
    public string Column { get; set; }

    [HtmlAttributeName("nw-page")]
    public long PageNumber { get; set; }

    public TableHeaderTagHelper(IHtmlGenerator generator) : base(generator)
    {
    }

    [HtmlAttributeName("nw-caption")]
    public string Caption { get; set; }
    
    [HtmlAttributeName("nw-parameters")]
    public IQueryParameters QueryParameters { set; get; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      if (string.IsNullOrWhiteSpace(Column))
      {
        return;
      }

      var span0 = new TagBuilder("span");
      var caption = string.IsNullOrWhiteSpace(Caption) ? Column.Humanize(LetterCasing.Title) : Caption;
      
      span0.InnerHtml.Append(caption);
      output.Content.AppendHtml(span0);

      var a = A();
      var span = Span();

      a.InnerHtml.AppendHtml(span);

      output.Content.AppendHtml(a);
    }

    private TagBuilder A()
    {

      var routeValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
      {
        {"order", Column}, {"page", PageNumber.ToString()}
      };

      if (Column == QueryParameters.OrderBy)
      {
        routeValues.Add("desc", (!QueryParameters.IsDescending).ToString().ToLower());
      }

      var routeValueDictionary = new RouteValueDictionary(routeValues);
      
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

    private bool IsSelected() => QueryParameters.OrderBy == Column;

    private bool IsDescending() => QueryParameters.IsDescending;
    
  }
  
}