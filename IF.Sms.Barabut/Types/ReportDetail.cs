using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace IF.Sms.Barabut
{
	public class ReportDetail
	{
		public List<ReportDetailItem> List
		{
			get;
			set;
		}

		public ReportDetail()
		{
			this.List = new List<ReportDetailItem>();
		}
	}
}