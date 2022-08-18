using FluentAssertions;
using Sat.Recruitment.Api.Services.CalculateTipUserStrategy;
using Xunit;

namespace Sat.Recruitment.Test.Services.TipUserStrategy
{
	public class SuperUserTipStrategyTest
	{
		private readonly ITipUserStrategy tipStrategy;

		public SuperUserTipStrategyTest()
		{
			tipStrategy = new SuperUserStrategy();
		}

		[Theory]
		[InlineData(100)]
		[InlineData(33)]
		public void OneHundredOrLessOfMoneyShouldReturnZeroAsTip(decimal money)
		{
			//arrange

			//act
			var tip = tipStrategy.CalculateTip(money);

			//assert
			tip.Should().Be(0);
		}

		[Theory]
		[InlineData(111)]
		[InlineData(2222)]
		public void MoneyGreatherThan100ShouldReturnMoneyMultiply2AsTip(decimal money)
		{
			//arrange
			decimal percentage = 0.2m;

			//act
			var tip = tipStrategy.CalculateTip(money);

			//assert
			tip.Should().Be(money * percentage);
		}
	}
}
