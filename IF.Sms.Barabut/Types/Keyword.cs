using System;
using System.Runtime.CompilerServices;

namespace IF.Sms.Barabut
{
	public class Keyword
	{
		public string ServiceNumber
		{
			get;
			set;
		}

		public DateTime Timestamp
		{
			get;
			set;
		}

		public int Validity
		{
			get;
			set;
		}

		public string Value
		{
			get;
			set;
		}

		public Keyword()
		{
		}
	}
}