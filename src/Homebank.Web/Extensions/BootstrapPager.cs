using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace Homebank.Web.Extensions
{
	public static class BootstrapPagerExtension
	{
        public static IHtmlContent Pager(this IHtmlHelper helper, int currentPage, Func<int, string> action, int total, int pageSize = 10, int numberOfLinks = 5) {
            var builder = new HtmlContentBuilder();

            if (total <= 0) {
                return builder;
            }

            var totalPages = (int)Math.Ceiling(total / (double)pageSize);
            var lastPageNumber = (int)Math.Ceiling((double)currentPage / numberOfLinks) * numberOfLinks;
            var firstPageNumber = lastPageNumber - (numberOfLinks - 1);
            var hasPreviousPage = currentPage > 1;
            var hasNextPage = currentPage < totalPages;

            if (lastPageNumber > totalPages)
            {
                lastPageNumber = totalPages;
            }

            builder.AppendHtmlLine("<ul class=\"pagination\">");

            builder.AppendHtml(addLink(1, action, currentPage == 1, "disabled", "<<", "First Page"));
            builder.AppendHtml(addLink(currentPage - 1, action, !hasPreviousPage, "disabled", "<", "Previous Page"));

            for (int i = firstPageNumber; i <= lastPageNumber; i++)
            {
                builder.AppendHtml(addLink(i, action, i == currentPage, "active", i.ToString(), i.ToString()));
            }

            builder.AppendHtml(addLink(currentPage + 1, action, !hasNextPage, "disabled", ">", "Next Page"));
            builder.AppendHtml(addLink(totalPages, action, currentPage == totalPages, "disabled", ">>", "Last Page"));

            builder.AppendHtmlLine("</ul>");

            return builder;
        }

        private static HtmlContentBuilder addLink(int index, Func<int, string> action, bool condition, string classToAdd, string text, string tooltip)
        {
            var builder = new HtmlContentBuilder();

            builder.AppendHtmlLine($"<li class=\"page-item { (condition ? classToAdd : string.Empty) }\">");
            builder.AppendHtmlLine($"<a class=\"page-link\" title=\"{tooltip}\" href=\"{ (!condition ? action(index) : "#") }\">{text}</a>");
            builder.AppendHtmlLine("</li>");

            return builder;
        }
	}
}