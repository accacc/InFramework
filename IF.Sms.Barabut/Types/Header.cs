using System;
using System.Runtime.CompilerServices;

namespace IF.Sms.Barabut
{
	public class Header
	{
		public string From
		{
			get;
			set;
		}

		public DateTime? ScheduledDeliveryTime
		{
			get;
			set;
		}

		public short ValidityPeriod
		{
			get;
			set;
		}

		public Header()
		{
		}
	}
}