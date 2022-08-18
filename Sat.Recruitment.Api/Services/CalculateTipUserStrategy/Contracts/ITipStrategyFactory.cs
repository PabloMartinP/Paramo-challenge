using static Sat.Recruitment.Api.Entity.Enums;

namespace Sat.Recruitment.Api.Services.CalculateTipUserStrategy.Contracts
{
	public interface ITipStrategyFactory
	{
		ITipUserStrategy GetTipStrategy(UserType userType);
	}
}
