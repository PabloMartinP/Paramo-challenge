using Sat.Recruitment.Api.Entity.Model;
using Sat.Recruitment.Api.Model.Users.Request;

namespace Sat.Recruitment.Api.Builders.Contracts
{
	public interface IUserBuilder
	{
		User BuildWith(CreateUserRequest request);
	}
}
