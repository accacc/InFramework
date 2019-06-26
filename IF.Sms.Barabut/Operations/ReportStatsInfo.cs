
using System;
using System.Runtime.CompilerServices;

namespace IF.Sms.Barabut
{
	public class ReportStatsInfo
	{
		public ReportStats Report
		{
			get;
			set;
		}

		public IF.Sms.Barabut.Status Status
		{
			get;
			set;
		}

		public ReportStatsInfo()
		{
			this.Report = new ReportStats();
			this.Status = new IF.Sms.Barabut.Status();
		}
	}
}