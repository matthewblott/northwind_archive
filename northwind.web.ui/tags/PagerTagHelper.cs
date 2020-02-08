using System.Linq;
using cloudscribe.Web.Pagination;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using northwind.services.types;

namespace northwind.web.ui.tags
{
  [HtmlTargetElement("nw-pager", Attributes = "cs-paging-pagenumber,cs-paging-totalitems")]
  public class PagerTagHelper : cloudscribe.Web.Pagination.PagerTagHelper
  {
    [HtmlAttributeName("nw-descending")]
    public bool IsDescending { get; set; }
    
    [HtmlAttributeName("nw-order")]
    public string OrderBy { get; set; }
    
    [HtmlAttributeName("nw-query-values")]
    public IQueryValues QueryValues { get; set; } = new QueryValues();
    
    public PagerTagHelper(IUrlHelperFactory urlHelperFactory, IBuildPaginationLinks linkBuilder = null) 
      : base(urlHelperFactory, linkBuilder)
    {
      PageNumberParam = "page";
      ShowFirstLast = false;
      SuppressEmptyNextPrev = true;
      RemoveNextPrevLinks = false;
      SuppressInActiveFirstLast = true;
      FirstPageText = "First Page";
      LastPageText = "Last Page";
      LiCurrentCssClass = string.Empty;
      LiOtherCssClass = string.Empty;
      LiNonActiveCssClass = string.Empty;
      LinkCurrentCssClass = "pagination-link is-current";
      LinkOtherCssClass = "pagination-link has-text-light";
      PreviousPageHtml = "<a href='#' class='pagination-link has-text-light'>Previous</a>";
      NextPageHtml = "<a href='#' class='pagination-link has-text-light'>Next page</a>";
      
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      RouteValues.Add("desc", IsDescending.ToString().ToLower());
      RouteValues.Add("order", OrderBy);

      foreach (var (key, value) in QueryValues.Where(kv => kv.Value != null))
      {
        RouteValues.Add(key, value.ToString());
      }
      
      output.PreElement.AppendHtmlLine("<nav class=\"pagination\" role=\"navigation\">");

      base.Process(context, output);

      output.PostElement.AppendHtmlLine("</nav>");

    }
    
  }
  
}