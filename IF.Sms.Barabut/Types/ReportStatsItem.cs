using System;
using System.Runtime.CompilerServices;

namespace IF.Sms.Barabut
{
	public class ReportStatsItem
	{
		public int Count
		{
			get;
			set;
		}

		public int Delivered
		{
			get;
			set;
		}

		public int Month
		{
			get;
			set;
		}

		public int Undelivered
		{
			get;
			set;
		}

		public int Year
		{
			get;
			set;
		}

		public ReportStatsItem()
		{
		}
	}
}