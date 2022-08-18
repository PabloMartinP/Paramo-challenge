using FluentAssertions;
using Sat.Recruitment.Api.Entity;
using Sat.Recruitment.Api.Services.CalculateTipUserStrategy;
using Sat.Recruitment.Api.Services.CalculateTipUserStrategy.Contracts;
using Xunit;

namespace Sat.Recruitment.Test.Services.TipUserStrategy
{
	public class TipStrategyFactoryTest
	{
		private readonly ITipStrategyFactory tipStrategyFactory;

		public TipStrategyFactoryTest()
		{
			tipStrategyFactory = new TipStrategyFactory();
		}

		[Fact]
		public void UnknowTypeUserShouldUserDefaultUserStrategy()
		{
			//arrange

			//act
			var tipStrategy = tipStrategyFactory.GetTipStrategy(Enums.UserType.Default);

			//assert
			tipStrategy.Should().BeOfType<DefaultUserStrategy>();
		}

		[Fact]
		public void NormalUserShouldUseNormalTipStrategy()
		{
			//arrange

			//act
			var tipStrategy = tipStrategyFactory.GetTipStrategy(Enums.UserType.Normal);

			//assert
			Assert.IsType<NormalUserStrategy>(tipStrategy);
			tipStrategy.Should().BeOfType<NormalUserStrategy>();
		}
		[Fact]
		public void SuperUserShouldUseSuperTipStrategy()
		{
			//arrange

			//act
			var tipStrategy = tipStrategyFactory.GetTipStrategy(Enums.UserType.SuperUser);

			//assert
			tipStrategy.Should().BeOfType<SuperUserStrategy>();
		}

		[Fact]
		public void PremiumUserUserShouldUsePremiumTipStrategy()
		{
			//arrange

			//act
			var tipStrategy = tipStrategyFactory.GetTipStrategy(Enums.UserType.PremiumUser);

			//assert
			tipStrategy.Should().BeOfType<PremiumUserStrategy>();
		}


	}
}
