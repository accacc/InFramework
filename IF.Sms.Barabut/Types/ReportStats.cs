using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace IF.Sms.Barabut
{
	public class ReportStats
	{
		public List<ReportStatsItem> List
		{
			get;
			set;
		}

		public ReportStats()
		{
			this.List = new List<ReportStatsItem>();
		}
	}
}