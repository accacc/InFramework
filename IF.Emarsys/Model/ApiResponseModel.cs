using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace IF.Emarsys.Model
{
	[Serializable]
	public class ApiResponseModel : ApiResponse
	{
		[JsonConverter(typeof(SingleOrArrayConverter<EmarsysDataKeyValue>))]
		[JsonProperty("data")]
		public List<EmarsysDataKeyValue> Data
		{
			get;
			set;
		}

		public ApiResponseModel()
		{
		}
	}
}