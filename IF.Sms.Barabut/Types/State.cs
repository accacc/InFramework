using System;

namespace IF.Sms.Barabut
{
	public enum State
	{
		Queued,
		Sent,
		Canceled,
		Sending,
		Invalid,
		Receiving,
		Debt,
		Passive
	}
}