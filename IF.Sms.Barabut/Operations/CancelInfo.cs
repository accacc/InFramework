
using System;
using System.Runtime.CompilerServices;

namespace IF.Sms.Barabut
{
	public class CancelInfo
	{
		public IF.Sms.Barabut.Status Status
		{
			get;
			set;
		}

		public CancelInfo()
		{
			this.Status = new IF.Sms.Barabut.Status();
		}
	}
}