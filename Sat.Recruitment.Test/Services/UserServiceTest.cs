using FluentAssertions;
using Moq;
using Sat.Recruitment.Api.Builders;
using Sat.Recruitment.Api.Builders.Contracts;
using Sat.Recruitment.Api.Entity.Model;
using Sat.Recruitment.Api.Entity.Repositories;
using Sat.Recruitment.Api.Model.Users.Request;
using Sat.Recruitment.Api.Model.Users.Request.Validations;
using Sat.Recruitment.Api.Services;
using Sat.Recruitment.Api.Services.CalculateTipUserStrategy;
using Sat.Recruitment.Api.Services.CalculateTipUserStrategy.Contracts;
using System;
using System.Threading.Tasks;
using Xunit;
using static Sat.Recruitment.Api.Entity.Enums;

namespace Sat.Recruitment.Test.Services
{
	public class UserServiceTest
    {
        private readonly CreateUserRequestValidator validator;
        private readonly IResultBuilder resultBuilder;
        private readonly IUserBuilder userBuilder;
        private readonly ITipStrategyFactory tipStrategy;

        public UserServiceTest()
		{
            validator = new CreateUserRequestValidator();
            userBuilder = new UserBuilder();
            resultBuilder = new ResultBuilder();

            var mockTipStrategy = new Mock<ITipStrategyFactory>();
            mockTipStrategy.Setup(p => p.GetTipStrategy(It.IsAny<UserType>()))
                .Returns(new NormalUserStrategy());

            tipStrategy = mockTipStrategy.Object;
        }

        [Fact]
        public async Task NewUserShouldCreateSuccessfull()
        {
            //arrange
            var mockUserRepository = new Mock<IUserRepository>();

            mockUserRepository.Setup(p =>
                p.Exists(It.IsAny<Func<User, bool>>()))
                .ReturnsAsync(false);

            var userService = new UserService(
                tipStrategy, 
                mockUserRepository.Object, 
                validator,
                resultBuilder,
                userBuilder
                );

            var createUserRequest = new CreateUserRequest
            {
                Money = 124,
                Email = "mike@gmail.com",
                Name = "Mike",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = UserType.Normal
            };

            //act
            var actionResult = await userService.CreateUser(createUserRequest);

            //assert
            actionResult.Errors.Should().BeNull();
            actionResult.IsSuccess.Should().BeTrue();
        }

        [Theory]
        [InlineData("testlj")]
        [InlineData("fakemail@@@")]
        public async Task UnformattedEmailShouldReturnEmailErrorMessage(string email)
        {
            //arrange        
            var mockUserRepository = new Mock<IUserRepository>();

            mockUserRepository.Setup(p =>
                p.Exists(It.IsAny<Func<User, bool>>()))
                .ReturnsAsync(false);

            var userService = new UserService(
                tipStrategy, 
                mockUserRepository.Object,
                validator,
                resultBuilder,
                userBuilder);

            var createUserRequest = new CreateUserRequest
            {
                Money = 124,
                Email = email,
                Name = "Mike",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = UserType.Normal
            };

            //act
            var actionResult = await userService.CreateUser(createUserRequest);

            //assert
            actionResult.IsSuccess.Should().BeFalse();
            actionResult.Errors.Should().Contain("Email is not a valid email address");
            actionResult.Errors.Should().HaveCount(1);
        }

        [Fact]
        public async Task ExistingUserShouldReturnUserDuplicated()
        {
            //arrange
            var mockUserRepository = new Mock<IUserRepository>();

            mockUserRepository.Setup(p =>
                p.Exists(It.IsAny<Func<User, bool>>()))
                .ReturnsAsync(true);

            var userService = new UserService(
                tipStrategy, 
                mockUserRepository.Object,
                validator,
                resultBuilder,
                userBuilder);

            var createUserRequest = new CreateUserRequest
            {
                Money = 124,
                Email = "mike@gmail.com",
                Name = "Mike",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = UserType.Normal
            };

            //act
            var actionResult = await userService.CreateUser(createUserRequest);

            //assert
            actionResult.IsSuccess.Should().BeFalse();

            actionResult.Errors.Should().Contain(Api.Common.Constants.UserConstants.ValidationMessages.UserDuplicated);
            actionResult.Errors.Should().HaveCount(1);
        }
    }
}
