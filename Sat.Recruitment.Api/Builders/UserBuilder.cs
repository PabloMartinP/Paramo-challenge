using Sat.Recruitment.Api.Builders.Contracts;
using Sat.Recruitment.Api.Entity.Model;
using Sat.Recruitment.Api.Model.Users.Request;

namespace Sat.Recruitment.Api.Builders
{
	public class UserBuilder : IUserBuilder
    {
		public User BuildWith(CreateUserRequest request)
		{
            return new User
            {
                Name = request.Name,
                Email = request.Email,
                Address = request.Address,
                Phone = request.Phone,
                UserType = request.UserType,
                Money = request.Money
            };
        }
	}
}
