using Sat.Recruitment.Api.Entity.Model;
using System;
using System.Collections.Generic;
using System.Text;
using static Sat.Recruitment.Api.Entity.Enums;

namespace Sat.Recruitment.Test.Constants
{
	public static class ConstantsTest
	{
		public static class UserTest
		{
			public readonly static List<User> Users = new List<User>
			{
				new User
				{
					Email = "Juan@marmol.com",
					Name = "Juan",
					Address = "Peru 2464",
					Phone = "+5491154762312",
					UserType = UserType.Normal, 
					Money = 1234
				},
				new User
				{
					Email = "Franco.Perez@gmail.com",
					Name = "Franco",
					Address = "Alvear y Colombres",
					Phone = "+534645213542",
					UserType = UserType.PremiumUser,
					Money = 112234
				},
				new User
				{
					Email = "Agustina@gmail.com",
					Name = "Agustina",
					Address = "Garay y Otra Calle",
					Phone = "+534645213542",
					UserType = UserType.SuperUser,
					Money = 112234
				},
			};


		}
	}
}
