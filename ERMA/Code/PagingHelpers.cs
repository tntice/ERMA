using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ERMA.Models;
using ERMA.Code;
using TACUtility;
using System.Web.Mvc;
using System.Text;

namespace ERMA.Code
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html,
                                                int PGID,
                                                PagingInfo pagingInfo,
                                                Func<int, string> pageUrl)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i) + "&PGID=" + PGID);
                tag.InnerHtml = i.ToString();
                if(i == pagingInfo.CurrentPage)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }
                tag.AddCssClass("btn btn-default");
                sb.Append(tag.ToString());
            }

            return MvcHtmlString.Create(sb.ToString());
        }
    }
}