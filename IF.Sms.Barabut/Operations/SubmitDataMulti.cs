
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace IF.Sms.Barabut
{
	public class SubmitDataMulti
	{
		public IF.Sms.Barabut.Credential Credential
		{
			get;
			set;
		}

		public List<DataEnvelope> Envelopes
		{
			get;
			set;
		}

		public IF.Sms.Barabut.Header Header
		{
			get;
			set;
		}

		public SubmitDataMulti()
		{
		}
	}
}