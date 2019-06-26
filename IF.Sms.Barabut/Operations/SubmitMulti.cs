
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace IF.Sms.Barabut
{
	public class SubmitMulti
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

		public List<Envelope> Envelopes
		{
			get;
			set;
		}

		public IF.Sms.Barabut.Header Header
		{
			get;
			set;
		}

		public SubmitMulti()
		{
		}
	}
}