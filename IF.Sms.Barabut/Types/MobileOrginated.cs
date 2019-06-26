using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace IF.Sms.Barabut
{
	public class MobileOrginated
	{
		public List<MessageItem> List
		{
			get;
			set;
		}

		public MobileOrginated()
		{
			this.List = new List<MessageItem>();
		}
	}
}