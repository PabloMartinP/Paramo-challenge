namespace Sat.Recruitment.Api.Common
{
	public static class Constants
	{
		public static class UserConstants
		{
			public static class CsvColumns
			{
				public const int Name = 0;
				public const int Email = 1;
				public const int Phone = 2;
				public const int Address = 3;
				public const int UserType = 4;
				public const int Money = 5;

				public const char Separator = ',';
			}

			public static class TipConstants
			{
				public const decimal MoneyGreatherThan = 100;
				
				public const decimal NormalUserLessThan = 10;
				public const decimal NormalUserUpperPercentage = 0.12m;
				public const decimal NormalUserLowerPercentage = 0.8m;
				
				public const decimal SuperUserPercentage = 0.20m;
				public const decimal PremiumUserMultipleBy = 2;
			}

			public static class ValidationMessages
			{
				public const string PropertyRequired = "{PropertyName} is required";
				public const string EmailValid = "{PropertyName} is not a valid email address";
				public const string UserDuplicated = "User is duplicated";
			}
		}
		
		public static class ExceptionMessages
		{
			public const string NormalizedEmail = "Cannot normalize email {0}";
		}

	}
}
