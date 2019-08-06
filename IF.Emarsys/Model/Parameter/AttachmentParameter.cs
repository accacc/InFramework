using IF.Emarsys.Helper;
using System;
using System.Runtime.CompilerServices;

namespace IF.Emarsys.Model.Parameter
{
	public class AttachmentParameter
	{
		public byte[] Data
		{
			get;
			set;
		}

		public IF.Emarsys.Helper.FileExtension FileExtension
		{
			get;
			set;
		}

		public string FileName
		{
			get;
			set;
		}

		public AttachmentParameter()
		{
		}
	}
}