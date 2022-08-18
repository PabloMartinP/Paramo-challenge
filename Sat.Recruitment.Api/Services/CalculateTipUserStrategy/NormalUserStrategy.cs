using static Sat.Recruitment.Api.Common.Constants.UserConstants;

namespace Sat.Recruitment.Api.Services.CalculateTipUserStrategy
{
	public class NormalUserStrategy : ITipUserStrategy
	{
		public decimal CalculateTip(decimal money)
		{
			decimal percentage = 0;

			if (money > TipConstants.MoneyGreatherThan)
			{
				percentage = TipConstants.NormalUserUpperPercentage;

			}
			if (money < TipConstants.MoneyGreatherThan && money > TipConstants.NormalUserLessThan)
			{
				percentage = TipConstants.NormalUserLowerPercentage;
			}

			return money * percentage;
		}
	}
}
