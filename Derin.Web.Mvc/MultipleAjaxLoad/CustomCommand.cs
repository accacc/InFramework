namespace Derin.Core.Mvc.MultipleAjaxLoad
{
	public class CustomCommand: MultipleLoadCommand
	{
		public CustomCommand(string commandVerb)
		{
			_commandVerb = commandVerb;
		}

		private readonly string _commandVerb;

		protected override string CommandVerb
		{
			get { return _commandVerb; }
		}
	}
}