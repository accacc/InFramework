
using System;
using System.Runtime.CompilerServices;

namespace IF.Sms.Barabut
{
	public class Cancel
	{
		public IF.Sms.Barabut.Credential Credential
		{
			get;
			set;
		}

		public long MessageId
		{
			get;
			set;
		}

		public Cancel()
		{
		}
	}
}