using FluentAssertions;
using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Entity.Model;
using Sat.Recruitment.Api.Entity.Repositories;
using Sat.Recruitment.Api.Model;
using Sat.Recruitment.Api.Model.Users.Request;
using Sat.Recruitment.Api.Services.CalculateTipUserStrategy;
using Sat.Recruitment.Api.Services.CalculateTipUserStrategy.Contracts;
using Sat.Recruitment.Api.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using static Sat.Recruitment.Api.Entity.Enums;

namespace Sat.Recruitment.Test.Controllers
{
	public class UserControllerTest
    {
        public UserControllerTest()
        {
        }

        [Fact]
        public async Task NewUserShouldReturns201Created()
        {
            //arrange
            var mockTipStrategy = new Mock<ITipStrategyFactory>();
            mockTipStrategy.Setup(p => p.GetTipStrategy(It.IsAny<UserType>()))
                    .Returns(new NormalUserStrategy());

            var mockUserRepository = new Mock<IUserRepository>();

            mockUserRepository.Setup(p =>
                    p.Exists(It.IsAny<Func<User, bool>>()))
                    .ReturnsAsync(false);

            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(p => p.CreateUser(It.IsAny<CreateUserRequest>()))
                .ReturnsAsync(new Api.Model.Result { IsSuccess = true });

            var createUserRequest = new CreateUserRequest
            {
                Money = 124,
                Email = "mike@gmail.com",
                Name = "Mike",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = UserType.Normal
            };
            var userController = new UsersController(mockUserService.Object);


            //act
            var actionResult = await userController.CreateUser(createUserRequest);
            var result = (Microsoft.AspNetCore.Mvc.ObjectResult)actionResult.Result;
            var value = (Result)((Microsoft.AspNetCore.Mvc.ObjectResult)actionResult.Result).Value;
            //assert
            result.StatusCode.Should().Be(201);
            value.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task DuplicatedUserShouldReturns400BadRequest()
        {
            //arrange
            var mockTipStrategy = new Mock<ITipStrategyFactory>();
            mockTipStrategy.Setup(p => p.GetTipStrategy(It.IsAny<UserType>()))
                    .Returns(new NormalUserStrategy());

            var mockUserRepository = new Mock<IUserRepository>();

            mockUserRepository.Setup(p =>
                    p.Exists(It.IsAny<Func<User, bool>>()))
                    .ReturnsAsync(false);

            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(p => p.CreateUser(It.IsAny<CreateUserRequest>()))
                .ReturnsAsync(new Api.Model.Result 
                { 
                    IsSuccess = false, 
                    Errors = new List<string>
					{
                        Api.Common.Constants.UserConstants.ValidationMessages.UserDuplicated
                    }
                });

            var createUserRequest = new CreateUserRequest
            {
                Money = 124,
                Email = "mike@gmail.com",
                Name = "Mike",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = UserType.Normal
            };
            var userController = new UsersController(mockUserService.Object);

            //act
            var actionResult = await userController.CreateUser(createUserRequest);
            
            //assert
            var value = ((Microsoft.AspNetCore.Mvc.ObjectResult)actionResult.Result).Value;
            var result = (Microsoft.AspNetCore.Mvc.ObjectResult)actionResult.Result;
            result.StatusCode.Should().Be(400);
            value.Should().Be(Api.Common.Constants.UserConstants.ValidationMessages.UserDuplicated);
        }
    }
}
