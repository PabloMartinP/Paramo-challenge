using static Sat.Recruitment.Api.Common.Constants.UserConstants;

namespace Sat.Recruitment.Api.Services.CalculateTipUserStrategy
{
	public class PremiumUserStrategy : ITipUserStrategy
	{
		public decimal CalculateTip(decimal money)
		{
			if (money > TipConstants.MoneyGreatherThan)
			{
				return money * TipConstants.PremiumUserMultipleBy;
			}

			return 0;
		}
	}
}
