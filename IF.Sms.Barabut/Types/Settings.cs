using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace IF.Sms.Barabut
{
	public class Settings
	{
		public IF.Sms.Barabut.Balance Balance
		{
			get;
			set;
		}

		public List<Keyword> Keywords
		{
			get;
			set;
		}

		public IF.Sms.Barabut.OperatorSettings OperatorSettings
		{
			get;
			set;
		}

		public List<Sender> Senders
		{
			get;
			set;
		}

		public Settings()
		{
			this.Senders = new List<Sender>();
			this.Keywords = new List<Keyword>();
			this.OperatorSettings = new IF.Sms.Barabut.OperatorSettings();
			this.Balance = new IF.Sms.Barabut.Balance();
		}
	}
}