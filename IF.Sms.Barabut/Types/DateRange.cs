using System;
using System.Runtime.CompilerServices;

namespace IF.Sms.Barabut
{
	public class DateRange
	{
		public DateTime? Begin
		{
			get;
			set;
		}

		public DateTime? End
		{
			get;
			set;
		}

		public DateRange()
		{
		}

		public void Validate()
		{
			DateTime now;
			if (!this.Begin.HasValue)
			{
				now = DateTime.Now;
				this.Begin = new DateTime?(now.Date);
			}
			if ((!this.End.HasValue || !(this.Begin.Value > this.End.Value) ? !this.End.HasValue : true))
			{
				now = this.Begin.Value;
				this.End = new DateTime?(now.AddDays(1));
			}
		}
	}
}