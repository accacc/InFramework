namespace IF.Sms.Barabut
{
    public class GetBalanceInfo
	{
		public IF.Sms.Barabut.Balance Balance
		{
			get;
			set;
		}

		public IF.Sms.Barabut.Status Status
		{
			get;
			set;
		}

		public GetBalanceInfo()
		{
			this.Balance = new IF.Sms.Barabut.Balance();
			this.Status = new IF.Sms.Barabut.Status();
		}
	}
}