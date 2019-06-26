
using System;
using System.Runtime.CompilerServices;

namespace IF.Sms.Barabut
{
	public class GetSettingsInfo
	{
		public IF.Sms.Barabut.Settings Settings
		{
			get;
			set;
		}

		public IF.Sms.Barabut.Status Status
		{
			get;
			set;
		}

		public GetSettingsInfo()
		{
			this.Settings = new IF.Sms.Barabut.Settings();
			this.Status = new IF.Sms.Barabut.Status();
		}
	}
}