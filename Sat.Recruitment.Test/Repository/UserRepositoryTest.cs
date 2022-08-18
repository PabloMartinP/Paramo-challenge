using FluentAssertions;
using Moq;
using Sat.Recruitment.Api.Entity.Model;
using Sat.Recruitment.Api.Entity.Repositories;
using Sat.Recruitment.Api.Entity.Repositories.Contracts;
using Sat.Recruitment.Test.Constants;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test.Repository
{
	public class UserRepositoryTest
	{
		[Fact]
		public async Task ExistingEmailShoulBeDuplicated()
		{
			//arrange
			var mockStream = new Mock<IDbUsers>();
			var users = ConstantsTest.UserTest.Users;
			mockStream.Setup(p => p.GetUsers()).ReturnsAsync(users);
			var userRepository = new UserRepository(mockStream.Object);

			var user = new User
			{
				Email = "Juan@marmol.com"
			};

			//act
			var isDuplicated = await userRepository.Exists(p => p.IsDuplicate(user));

			//assert
			isDuplicated.Should().BeTrue();			
		}

		[Fact]
		public async Task NotExistingEmailShoulNotBeDuplicated()
		{
			//arrange
			var mockStream = new Mock<IDbUsers>();
			var users = ConstantsTest.UserTest.Users;
			mockStream.Setup(p => p.GetUsers()).ReturnsAsync(users);
			var userRepo = new UserRepository(mockStream.Object);

			var user = new User
			{
				Email = "pipe@marmol.com"
			};

			//act
			var isDuplicated = await userRepo.Exists(p => p.IsDuplicate(user));

			//assert
			isDuplicated.Should().BeFalse();
		}
	}
}
