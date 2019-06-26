using System;
using System.Web.Mvc;

namespace Derin.Core.Mvc.MultipleAjaxLoad
{
	public class MultipleLoadAjaxContent
	{
		public MultipleLoadAjaxContent(string selector, MultipleLoadCommand updateCommand, Func<System.Web.Mvc.ActionResult> getResult)
		{
			Command = updateCommand;
			Selector = selector;
			GetResult = getResult;
		}

		public string Selector { get; set; }
		public MultipleLoadCommand Command { get; set; }
		public Func<System.Web.Mvc.ActionResult> GetResult { get; set; }
	}
}