
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace IF.Sms.Barabut
{
	public class SubmitData
	{
		public IF.Sms.Barabut.Credential Credential
		{
			get;
			set;
		}

		public List<DataItem> Data
		{
			get;
			set;
		}

		public IF.Sms.Barabut.Header Header
		{
			get;
			set;
		}

		public List<string> To
		{
			get;
			set;
		}

		public SubmitData()
		{
		}
	}
}