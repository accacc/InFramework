using System;
using System.Runtime.CompilerServices;

namespace IF.Sms.Barabut
{
	public class ReportDetailItem : DataItem
	{
		public decimal Cost
		{
			get;
			set;
		}

		public int ErrorCode
		{
			get;
			set;
		}

		public long Id
		{
			get;
			set;
		}

		public DateTime LastUpdated
		{
			get;
			set;
		}

		public string MSISDN
		{
			get;
			set;
		}

		public short Network
		{
			get;
			set;
		}

		public byte Sequence
		{
			get;
			set;
		}

		public DlrState State
		{
			get;
			set;
		}

		public DateTime Submitted
		{
			get;
			set;
		}

		public ReportDetailItem()
		{
		}
	}
}