
using System;
using System.Runtime.CompilerServices;

namespace IF.Sms.Barabut
{
	public class SubmitInfo
	{
		public long MessageId
		{
			get;
			set;
		}

		public IF.Sms.Barabut.Status Status
		{
			get;
			set;
		}

		public SubmitInfo()
		{
			this.Status = new IF.Sms.Barabut.Status();
		}
	}
}