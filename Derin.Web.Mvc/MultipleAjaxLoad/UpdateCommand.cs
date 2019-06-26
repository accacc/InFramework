namespace Derin.Core.Mvc.MultipleAjaxLoad
{
	public class UpdateCommand: MultipleLoadCommand
	{
		protected override string CommandVerb { get { return "update"; } }
	}
}