
using System;
using System.Runtime.CompilerServices;

namespace IF.Sms.Barabut
{
	public class GetMo
	{
		public IF.Sms.Barabut.Credential Credential
		{
			get;
			set;
		}

		public DateRange Range
		{
			get;
			set;
		}

		public string Recipient
		{
			get;
			set;
		}

		public InboxState State
		{
			get;
			set;
		}

		public GetMo()
		{
		}
	}
}