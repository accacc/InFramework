using System;
using System.Runtime.CompilerServices;

namespace IF.Sms.Barabut
{
	public class ReportItem
	{
		public DataCoding Coding
		{
			get;
			set;
		}

		public decimal Cost
		{
			get;
			set;
		}

		public int Count
		{
			get;
			set;
		}

		public int DeliveredCount
		{
			get;
			set;
		}

		public long Id
		{
			get;
			set;
		}

		public string Message
		{
			get;
			set;
		}

		public DateTime Received
		{
			get;
			set;
		}

		public string Sender
		{
			get;
			set;
		}

		public DateTime Sent
		{
			get;
			set;
		}

		public IF.Sms.Barabut.State State
		{
			get;
			set;
		}

		public int UndeliveredCount
		{
			get;
			set;
		}

		public ReportItem()
		{
		}
	}
}