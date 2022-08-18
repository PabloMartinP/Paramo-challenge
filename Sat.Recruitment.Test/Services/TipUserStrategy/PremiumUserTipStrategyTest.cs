using FluentAssertions;
using Sat.Recruitment.Api.Services.CalculateTipUserStrategy;
using Xunit;

namespace Sat.Recruitment.Test.Services.TipUserStrategy
{
	public class PremiumUserTipStrategyTest
	{
		private readonly ITipUserStrategy tipStrategy;

		public PremiumUserTipStrategyTest()
		{
			tipStrategy = new PremiumUserStrategy();
		}

		[Theory]
		[InlineData(120)]
		[InlineData(770)]
		public void MoneyGreatherThan100ReturnMoneyMultiply2(decimal money)
		{
			//arrange

			//act
			var tip = tipStrategy.CalculateTip(money);

			//assert
			tip.Should().Be(money * 2);
		}

		[Theory]
		[InlineData(100)]
		[InlineData(33)]
		public void MoneyOf100OrLessReturnsZero(decimal money)
		{
			//arrange

			//act
			var tip = tipStrategy.CalculateTip(money);

			//assert
			tip.Should().Be(0);
		}
	}
}
