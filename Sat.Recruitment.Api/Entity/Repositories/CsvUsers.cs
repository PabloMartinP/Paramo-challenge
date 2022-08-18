using Microsoft.Extensions.Configuration;
using Sat.Recruitment.Api.Entity.Model;
using Sat.Recruitment.Api.Entity.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using static Sat.Recruitment.Api.Common.Constants;
using static Sat.Recruitment.Api.Entity.Enums;

namespace Sat.Recruitment.Api.Entity.Repositories
{
	public class CsvUsers : IDbUsers
	{
		private readonly IConfiguration configuration;

		public CsvUsers(IConfiguration configuration)
		{
			this.configuration = configuration;			
		}

		public async Task<List<User>> GetUsers()
		{
			var relativePath = configuration.GetValue<string>("Database:PathCsvUsers");
			var path = Directory.GetCurrentDirectory() + relativePath;

			using (var fileStream = new FileStream(path, FileMode.Open))
			{
				using (var reader = new StreamReader(fileStream))
				{
					var users = new List<User>();

					while (!reader.EndOfStream)
					{
						var line = await reader.ReadLineAsync();

						var user = LineToUser(line);

						users.Add(user);
					}

					return users;
				}
				
			}
		}

		private User LineToUser(string line)
		{
			var splittedLine = line.Split(UserConstants.CsvColumns.Separator);

			Enum.TryParse(splittedLine[UserConstants.CsvColumns.UserType].ToString(), out UserType userTypeConverted);

			var user = new User
			{
				Name = splittedLine[UserConstants.CsvColumns.Name],
				Email = splittedLine[UserConstants.CsvColumns.Email],
				Phone = splittedLine[UserConstants.CsvColumns.Phone],
				Address = splittedLine[UserConstants.CsvColumns.Address],
				UserType = userTypeConverted,
				Money = decimal.Parse(splittedLine[UserConstants.CsvColumns.Money]),
			};

			return user;
		}
	}
}
