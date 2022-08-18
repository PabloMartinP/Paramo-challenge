using FluentAssertions;
using Sat.Recruitment.Api.Services.CalculateTipUserStrategy;
using Xunit;

namespace Sat.Recruitment.Test.Services.TipUserStrategy
{
	public class NormalUserTipStrategyTest
	{
		private readonly ITipUserStrategy tipStrategy;

		public NormalUserTipStrategyTest()
		{
			tipStrategy = new NormalUserStrategy();
		}

		[Theory]
		[InlineData(120)]
		[InlineData(770)]
		public void MoneyGreatherThan100ShouldReturnDot12PercentageOfMoneyAsTip(decimal money)
		{
			//arrange
			decimal percentage = 0.12m;

			//act
			var tip = tipStrategy.CalculateTip(money);

			//assert
			tip.Should().Be(money * percentage);
		}

		[Theory]
		[InlineData(20)]
		[InlineData(80)]
		public void MoneyBetween10And100ShouldReturnDot8PercentageOfMoneyAsTip(decimal money)
		{
			//arrange
			decimal percentage = 0.8m;

			//act
			var tip = tipStrategy.CalculateTip(money);

			//assert
			tip.Should().Be(money * percentage);
		}

		[Fact]
		public void With100OfMoneyShouldReturns0AsTip()
		{
			//arrange

			//act
			var tip = tipStrategy.CalculateTip(100);

			//assert
			tip.Should().Be(0);
		}
	}
}
