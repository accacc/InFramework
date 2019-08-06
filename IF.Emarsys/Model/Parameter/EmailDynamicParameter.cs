using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace IF.Emarsys.Model.Parameter
{
	public class EmailDynamicParameter
	{
		[JsonProperty("global")]
		public Dictionary<string, string> Global
		{
			get;
			set;
		}

		public EmailDynamicParameter()
		{
		}
	}
}