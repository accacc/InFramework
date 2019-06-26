using System;
using System.Runtime.CompilerServices;

namespace IF.Sms.Barabut
{
	public class OperatorSettings
	{
		public IF.Sms.Barabut.Account Account
		{
			get;
			set;
		}

		public string MSISDN
		{
			get;
			set;
		}

		public string ServiceId
		{
			get;
			set;
		}

		public decimal UnitPrice
		{
			get;
			set;
		}

		public string VariantId
		{
			get;
			set;
		}

		public OperatorSettings()
		{
		}
	}
}