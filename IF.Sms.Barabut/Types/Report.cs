using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace IF.Sms.Barabut
{
	public class Report
	{
		public List<ReportItem> List
		{
			get;
			set;
		}

		public Report()
		{
			this.List = new List<ReportItem>();
		}
	}
}