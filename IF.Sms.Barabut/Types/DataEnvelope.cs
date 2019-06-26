using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace IF.Sms.Barabut
{
	public class DataEnvelope
	{
		public List<DataItem> Data
		{
			get;
			set;
		}

		public string To
		{
			get;
			set;
		}

		public DataEnvelope()
		{
		}
	}
}