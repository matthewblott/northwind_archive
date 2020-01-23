using cloudscribe.Web.Pagination;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace northwind.web.ui.tags
{
  [HtmlTargetElement("nw-pager", Attributes = "cs-paging-pagenumber,cs-paging-totalitems")]
  public class PagerTagHelper : cloudscribe.Web.Pagination.PagerTagHelper
  {
    [HtmlAttributeName("asp-descending")]
    public bool Descending { get; set; }
    
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
      RouteValues.Add("desc", Descending.ToString().ToLower());
      
      base.Process(context, output);
    }
    
  }
  
}