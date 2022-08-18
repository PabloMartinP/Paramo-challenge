using FluentAssertions;
using Moq;
using Sat.Recruitment.Api.Entity.Model;
using Sat.Recruitment.Api.Entity.Repositories;
using Sat.Recruitment.Api.Model.Users.Request;
using Sat.Recruitment.Api.Model.Users.Request.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test.Validations
{
	public class CreateUserRequestValidatorTest
	{
		private readonly CreateUserRequestValidator validator;

		public CreateUserRequestValidatorTest()
		{
			validator = new CreateUserRequestValidator();
		}

		[Fact]
		public async Task UserWithEmptyPropertiesShouldFail()
		{
			//arrange
			var user = new CreateUserRequest
			{

			};

			//act
			var validationResult = await validator.ValidateAsync(user);

			//assert
			validationResult.IsValid.Should().BeFalse();
		}

		[Fact]
		public async Task UserWithEmptyEmailShouldReturnError3Errors()
		{
			//arrange
			var user = new CreateUserRequest
			{
				UserType = Api.Entity.Enums.UserType.Normal, 
				Email = "test@gmail.com", 
			};

			//act
			var validationResult = await validator.ValidateAsync(user);

			//assert
			validationResult.Errors.Should().HaveCount(3);
		}

		[Fact]
		public async Task UserWithEmptyPropertiesShouldReturn4Errors()
		{
			//arrange
			var user = new CreateUserRequest
			{

			};

			//act
			var validationResult = await validator.ValidateAsync(user);

			//assert
			validationResult.Errors.Should().HaveCount(4);
		}

		[Fact]
		public async Task UserWithEmptyPropertiesShouldReturn4ErrorsWithMesssage()
		{
			//arrange
			var user = new CreateUserRequest
			{

			};

			//act
			var validationResult = await validator.ValidateAsync(user);


			//assert
			var errorsResult = new List<string>()
			{
				"Name is required", 
				"Email is required",
				"Address is required", 
				"Phone is required", 
			};
			validationResult.Errors.Select(p=>p.ErrorMessage).ToList().Should().BeEquivalentTo(errorsResult);
		}

		[Fact]
		public async Task UserWithNullPropertiesShouldFail()
		{
			//arrange
			var user = new CreateUserRequest
			{
				Address = null,
				Email = null,
				Name = null,
				Phone = null,
			};

			//act
			var validationResult = await validator.ValidateAsync(user);

			//assert
			validationResult.IsValid.Should().BeFalse();
		}

		[Fact]
		public async Task UserWithAllPropertiesShouldBeOk()
		{
			//arrange
			var user = new CreateUserRequest
			{
				Address = "145 street",
				Email = "test@mail.com",
				Name = "martin",
				Phone = "pe",
			};
			
			//act
			var validationResult = await validator.ValidateAsync(user);

			//assert
			validationResult.IsValid.Should().BeTrue();
		}

		[Fact]
		public async Task UserWithBadEmailFormatShouldFail()
		{
			//arrange
			var user = new CreateUserRequest
			{
				Address = "145 street",
				Email = "testttt",
				Name = "martin",
				Phone = "pe",
			};

			//act
			var validationResult = await validator.ValidateAsync(user);

			//assert
			validationResult.IsValid.Should().BeFalse();
		}
		[Fact]
		public async Task UserWithBadEmailFormatShouldReturnMessageBadFormatEmail()
		{
			//arrange
			var user = new CreateUserRequest
			{
				Address = "145 street",
				Email = "testttt",
				Name = "martin",
				Phone = "pe",
			};

			//act
			var validationResult = await validator.ValidateAsync(user);

			//assert
			validationResult.Errors.Select(p => p.ErrorMessage)
				.Should().Contain("Email is not a valid email address");
		}

		[Fact]
		public async Task UserWithNullPropertiesShouldReturn4Errors()
		{
			//arrange
			var user = new CreateUserRequest
			{
				Address = null,
				Email = null,
				Name = null,
				Phone = null,
			};

			//act
			var validationResult = await validator.ValidateAsync(user);

			//assert
			validationResult.Errors.Should().HaveCount(4);
		}
	}
}
