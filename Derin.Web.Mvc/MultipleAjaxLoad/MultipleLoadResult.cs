using System.Collections.Generic;
using System.Net.Mime;
using System.Web.Mvc;

namespace Derin.Core.Mvc.MultipleAjaxLoad
{
	public class MultipleLoadResult : System.Web.Mvc.ActionResult
	{
		public IEnumerable<MultipleLoadAjaxContent> ContentItems { get; private set; }

		public MultipleLoadResult(params MultipleLoadAjaxContent[] contentItems)
		{
			ContentItems = new List<MultipleLoadAjaxContent>();
			((List<MultipleLoadAjaxContent>)ContentItems).AddRange(contentItems);
		}

		public override void ExecuteResult(ControllerContext context)
		{
			context.HttpContext.Response.ContentType = MediaTypeNames.Text.Html;

			context.HttpContext.Response.Write("<div class=\"ajax-response\">");
			foreach (var item in ContentItems)
			{
				context.HttpContext.Response.Write(string.Format("<div class=\"ajax-content\" title=\"{0} {1}\">",
				                                                 item.Command.CommandText, item.Selector));
				item.GetResult().ExecuteResult(context);
				context.HttpContext.Response.Write("</div>");
			}
			context.HttpContext.Response.Write( "</div>");
		}
	}
}