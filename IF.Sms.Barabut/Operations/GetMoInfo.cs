
using System;
using System.Runtime.CompilerServices;

namespace IF.Sms.Barabut
{
	public class GetMoInfo
	{
		public MobileOrginated Message
		{
			get;
			set;
		}

		public IF.Sms.Barabut.Status Status
		{
			get;
			set;
		}

		public GetMoInfo()
		{
			this.Message = new MobileOrginated();
			this.Status = new IF.Sms.Barabut.Status();
		}
	}
}