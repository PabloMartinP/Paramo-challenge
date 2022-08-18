using Sat.Recruitment.Api.Entity.Model;
using Sat.Recruitment.Api.Entity.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static Sat.Recruitment.Api.Common.Constants;
using static Sat.Recruitment.Api.Entity.Enums;

namespace Sat.Recruitment.Api.Entity.Repositories
{
	public class UserRepository : IUserRepository
	{
		private List<User> users;
		private readonly IDbUsers csvUsers;

		public UserRepository(IDbUsers streamCsv)
		{
			users = new List<User>();
			this.csvUsers = streamCsv;
		}

		private async Task Populate()
		{
			if (users.Any())
				return;
			
			users = await csvUsers.GetUsers();
		}

		public async Task<bool> Exists(Func<User, bool> expression = null)
		{
			await Populate();

			return users.Any(expression);
		}
	}
}
