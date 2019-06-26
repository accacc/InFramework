
using System;
using System.Runtime.CompilerServices;

namespace IF.Sms.Barabut
{
	public class QueryMulti
	{
		public IF.Sms.Barabut.Credential Credential
		{
			get;
			set;
		}

		public DateRange Range
		{
			get;
			set;
		}

		public QueryMulti()
		{
		}
	}
}