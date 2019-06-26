using System;
using System.Runtime.CompilerServices;

namespace IF.Sms.Barabut
{
	public class Identifier
	{
		public int Id
		{
			get;
			set;
		}

		public int OwnerId
		{
			get;
			set;
		}

		public int UserId
		{
			get;
			set;
		}

		public Identifier()
		{
			this.OwnerId = -1;
		}
	}
}