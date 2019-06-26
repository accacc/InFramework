
using System;
using System.Runtime.CompilerServices;

namespace IF.Sms.Barabut
{
	public class LoginInfo
	{
		public IF.Sms.Barabut.Identifier Identifier
		{
			get;
			set;
		}

		public IF.Sms.Barabut.Status Status
		{
			get;
			set;
		}

		public LoginInfo()
		{
			this.Identifier = new IF.Sms.Barabut.Identifier();
			this.Status = new IF.Sms.Barabut.Status();
		}
	}
}