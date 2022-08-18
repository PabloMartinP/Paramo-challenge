using System.ComponentModel;

namespace Sat.Recruitment.Api.Entity
{
	public class Enums
	{
		public enum UserType
		{
			Default = 0,
			[Description("Normal")]
			Normal = 1,
			[Description("SuperUser")]
			SuperUser = 2,
			[Description("PremiumUser")]
			PremiumUser = 3
		}
	}
}
