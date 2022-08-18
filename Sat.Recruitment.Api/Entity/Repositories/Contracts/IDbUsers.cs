using Sat.Recruitment.Api.Entity.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Entity.Repositories.Contracts
{
	public interface IDbUsers
	{
		Task<List<User>> GetUsers();
	}
}
