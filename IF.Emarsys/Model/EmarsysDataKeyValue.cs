using System;
using System.Runtime.CompilerServices;

namespace IF.Emarsys.Model
{
	[Serializable]
	public class EmarsysDataKeyValue
	{
		public int Id
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public EmarsysDataKeyValue()
		{
		}
	}
}