using static Sat.Recruitment.Api.Common.Constants.UserConstants;

namespace Sat.Recruitment.Api.Services.CalculateTipUserStrategy
{
	public class SuperUserStrategy : ITipUserStrategy
	{
		public decimal CalculateTip(decimal money)
		{
			if (money > TipConstants.MoneyGreatherThan)
			{
				var percentage = TipConstants.SuperUserPercentage;
				
				return money * percentage;
			}

			return 0;
		}
	}
}
