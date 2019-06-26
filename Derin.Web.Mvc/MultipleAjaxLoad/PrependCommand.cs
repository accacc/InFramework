namespace Derin.Core.Mvc.MultipleAjaxLoad
{
	public class PrependCommand: MultipleLoadCommand
	{
		protected override string CommandVerb { get { return "prependTo"; } }
	}
}