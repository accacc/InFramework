
using System;
using System.Runtime.CompilerServices;

namespace IF.Sms.Barabut
{
	public class ReportInfo
	{
		public IF.Sms.Barabut.Report Report
		{
			get;
			set;
		}

		public IF.Sms.Barabut.Status Status
		{
			get;
			set;
		}

		public ReportInfo()
		{
			this.Report = new IF.Sms.Barabut.Report();
			this.Status = new IF.Sms.Barabut.Status();
		}
	}
}