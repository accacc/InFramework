
using System;
using System.Runtime.CompilerServices;

namespace IF.Sms.Barabut
{
	public class ReportDetailInfo
	{
		public IF.Sms.Barabut.ReportDetail ReportDetail
		{
			get;
			set;
		}

		public IF.Sms.Barabut.Status Status
		{
			get;
			set;
		}

		public ReportDetailInfo()
		{
			this.ReportDetail = new IF.Sms.Barabut.ReportDetail();
			this.Status = new IF.Sms.Barabut.Status();
		}
	}
}