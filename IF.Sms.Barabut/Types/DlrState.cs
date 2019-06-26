using System;

namespace IF.Sms.Barabut
{
	public enum DlrState
	{
		Scheduled,
		Enroute,
		Delivered,
		Expired,
		Deleted,
		Undeliverable,
		Accepted,
		Unknown,
		Rejected,
		Skipped
	}
}