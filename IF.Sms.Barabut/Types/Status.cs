using System;
using System.Runtime.CompilerServices;

namespace IF.Sms.Barabut
{
	public class Status
	{
		public int Code
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		public Status()
		{
			this.Code = -1;
			this.Description = "Client error";
		}
	}
}