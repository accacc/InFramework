using System;
using System.Runtime.CompilerServices;

namespace IF.Emarsys.Model
{
	[Serializable]
	public class ApiResponse
	{
		public int ReplyCode
		{
			get;
			set;
		}

		public string ReplyText
		{
			get;
			set;
		}

		public ApiResponse()
		{
		}
	}
}