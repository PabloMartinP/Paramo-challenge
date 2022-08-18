using Sat.Recruitment.Api.Services.CalculateTipUserStrategy.Contracts;
using static Sat.Recruitment.Api.Entity.Enums;

namespace Sat.Recruitment.Api.Services.CalculateTipUserStrategy
{
	public class TipStrategyFactory : ITipStrategyFactory
    {
		public ITipUserStrategy GetTipStrategy(UserType userType)
		{
			return userType switch
			{
				UserType.Normal => new NormalUserStrategy(),
				UserType.SuperUser => new SuperUserStrategy(),
				UserType.PremiumUser => new PremiumUserStrategy(),
				_ => new DefaultUserStrategy(),
			};
		}
	}
}
