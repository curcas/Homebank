using System;
using System.Web.Mvc;

namespace Homebank.Web.Extensions
{
	public static class BootstrapPagerExtension
	{
		public static MvcHtmlString BootstrapPager(this HtmlHelper helper, int currentPageIndex, Func<int, string> action, int totalItems, int pageSize = 10, int numberOfLinks = 5)
		{
			if (totalItems <= 0)
			{
				return MvcHtmlString.Empty;
			}

			var totalPages = (int) Math.Ceiling(totalItems/(double) pageSize);
			var lastPageNumber = (int) Math.Ceiling((double) currentPageIndex/numberOfLinks)*numberOfLinks;
			var firstPageNumber = lastPageNumber - (numberOfLinks - 1);
			var hasPreviousPage = currentPageIndex > 1;
			var hasNextPage = currentPageIndex < totalPages;

			if (lastPageNumber > totalPages)
			{
				lastPageNumber = totalPages;
			}

			var ul = new TagBuilder("ul");
			ul.AddCssClass("pagination");
			ul.InnerHtml += addLink(1, action, currentPageIndex == 1, "disabled", "<<", "First Page");
			ul.InnerHtml += addLink(currentPageIndex - 1, action, !hasPreviousPage, "disabled", "<", "Previous Page");

			for (int i = firstPageNumber; i <= lastPageNumber; i++)
			{
				ul.InnerHtml += addLink(i, action, i == currentPageIndex, "active", i.ToString(), i.ToString());
			}

			ul.InnerHtml += addLink(currentPageIndex + 1, action, !hasNextPage, "disabled", ">", "Next Page");
			ul.InnerHtml += addLink(totalPages, action, currentPageIndex == totalPages, "disabled", ">>", "Last Page");

			return MvcHtmlString.Create(ul.ToString());
		}
		
		private static TagBuilder addLink(int index, Func<int, string> action, bool condition, string classToAdd, string linkText, string tooltip)
		{
			var li = new TagBuilder("li");
			li.MergeAttribute("title", tooltip);

			if (condition)
			{
				li.AddCssClass(classToAdd);
			}

			var a = new TagBuilder("a");
			a.MergeAttribute("href", !condition ? action(index) : "javascript:");
			a.SetInnerText(linkText);

			li.InnerHtml = a.ToString();
			return li;
		}

		private static double getMiddlePage(double pages)
		{
			return Math.Ceiling(pages / 2);
		}

		private static int getFirstPage(int current, int pageSize)
		{
			var first = 0;

			if (current > getMiddlePage(pageSize))
			{

			}

			if (first < 0)
			{
				first = 0;
			}

			return first;
		}
	}
}