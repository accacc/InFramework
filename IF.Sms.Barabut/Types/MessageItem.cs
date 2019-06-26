using System;
using System.Runtime.CompilerServices;

namespace IF.Sms.Barabut
{
	public class MessageItem
	{
		public DateTime? Forwarded
		{
			get;
			set;
		}

		public long Id
		{
			get;
			set;
		}

		public string Keyword
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

		public decimal Price
		{
			get;
			set;
		}

		public DateTime Received
		{
			get;
			set;
		}

		public string Recipient
		{
			get;
			set;
		}

		public string Text
		{
			get;
			set;
		}

		public string Xser
		{
			get;
			set;
		}

		public MessageItem()
		{
		}
	}
}