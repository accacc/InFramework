namespace Derin.Core.Mvc.MultipleAjaxLoad
{
	public class AppendCommand: MultipleLoadCommand
	{
		protected override string CommandVerb { get { return "appendTo"; } }
	}
}