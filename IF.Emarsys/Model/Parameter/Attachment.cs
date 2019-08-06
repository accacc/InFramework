using Newtonsoft.Json;
using System;
using System.Runtime.CompilerServices;

namespace IF.Emarsys.Model.Parameter
{
	internal class Attachment
	{
		[JsonProperty("data")]
		public byte[] Data
		{
			get;
			set;
		}

		[JsonProperty("filename")]
		public string FileName
		{
			get;
			set;
		}

		public Attachment()
		{
		}
	}
}