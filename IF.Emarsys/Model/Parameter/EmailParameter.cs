using Newtonsoft.Json;
using System;
using System.Runtime.CompilerServices;

namespace IF.Emarsys.Model.Parameter
{
	public class EmailParameter
	{
		[JsonProperty("attachment")]
		public object Attachments
		{
			get;
			set;
		}

		[JsonProperty("contacts")]
		public object Contacts
		{
			get;
			set;
		}

		[JsonProperty("data")]
		public object Data
		{
			get;
			set;
		}

		[JsonProperty("external_id")]
		public string ExternalId
		{
			get;
			set;
		}

		[JsonProperty("key_id")]
		public string KeyId
		{
			get;
			set;
		}

		public EmailParameter()
		{
		}
	}
}