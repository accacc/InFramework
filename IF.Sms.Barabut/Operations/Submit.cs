
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace IF.Sms.Barabut
{
	public class Submit
	{
		public IF.Sms.Barabut.Credential Credential
		{
			get;
			set;
		}

		public IF.Sms.Barabut.DataCoding DataCoding
		{
			get;
			set;
		}

		public IF.Sms.Barabut.Header Header
		{
			get;
			set;
		}

		public string Message
		{
			get;
			set;
		}

		public List<string> To
		{
			get;
			set;
		}

		public Submit()
		{
		}
	}
}