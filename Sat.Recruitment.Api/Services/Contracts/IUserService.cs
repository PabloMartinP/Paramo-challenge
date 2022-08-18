using Sat.Recruitment.Api.Model;
using Sat.Recruitment.Api.Model.Users.Request;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Services.Contracts
{
	public interface IUserService
	{
		Task<Result> CreateUser(CreateUserRequest request);
	}
}
