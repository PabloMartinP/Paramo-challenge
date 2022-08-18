using FluentAssertions;
using Sat.Recruitment.Api.Entity.Model;
using Sat.Recruitment.Api.Exceptions;
using System;
using Xunit;
using static Sat.Recruitment.Api.Common.Constants;

namespace Sat.Recruitment.Test.Entities
{
	public  class UserTest
	{
		[Theory]
		[InlineData("test@mail.com")]
		public void UserWithSameEmailShouldBeDuplicated(string email)
		{
			//arrange
			var user = new User
			{
				Email = email,
				Phone = "54 11 23455323",
				Name = "steve",
				Address = "324 av"
			};
			var anotherUser = new User
			{
				Email = email,
				Phone = "54 11 23455293",
				Name = "oliver",
				Address = "998 street"
			};

			//act
			var isDuplicated = user.IsDuplicate(anotherUser);

			//assert
			isDuplicated.Should().BeTrue();
		}

		[Theory]
		[InlineData("+54 11 12344321")]
		public void UserWithSamePhoneShouldBeDuplicated(string phone)
		{
			//arrange
			var user = new User
			{
				Email = "test@gmail.com",
				Phone = phone,
				Name = "steve",
				Address = "324 av"
			};
			var anotherUser = new User
			{
				Email = "blast@gmail.com",
				Phone = phone,
				Name = "oliver",
				Address = "998 street"
			};

			//act
			var isDuplicated = user.IsDuplicate(anotherUser);

			//assert
			isDuplicated.Should().BeTrue();
		}

		[Theory]
		[InlineData("juan perez", "123 street")]
		public void UserWithSameNameAndAddressShouldBeDuplicated(string name, string address)
		{
			//arrange
			var user = new User
			{
				Email = "test@gmail.com",
				Phone = "123432",
				Name = name,
				Address = address
			};
			var anotherUser = new User
			{
				Email = "blast@gmail.com",
				Phone = "9933322",
				Name = name,
				Address = address
			};

			//act
			var isDuplicated = user.IsDuplicate(anotherUser);

			//assert
			isDuplicated.Should().BeTrue();
		}
		[Theory]
		[InlineData("juan perez", "123 elm")]
		public void UserWithSameNameAndAddressShouldNotBeDuplicated(string name, string address)
		{
			//arrange
			var user = new User
			{
				Email = "test@gmail.com",
				Phone = "123432",
				Name = name,
				Address = address
			};
			var anotherUser = new User
			{
				Email = "blast@gmail.com",
				Phone = "9933322",
				Name = name + " street",
				Address = address + ". "
			};

			//act
			var isDuplicated = user.IsDuplicate(anotherUser);

			//assert
			isDuplicated.Should().BeFalse();
		}

		[Theory]
		[InlineData("Agustina@gmail.com")]
		[InlineData("pepe@gmail.com")]
		[InlineData("jose@gmail.com")]
		public void NormalizedEmailShoudNotChange(string email)
		{
			//arrange
			var user = new User
			{
				Email = email,
			};

			//act

			//assert
			user.NormalizedEmail.Should().Be(email);
		}
		
		[Fact]
		public void EmptyEmailShoudNotNormalizeEmail()
		{
			//arrange
			var user = new User
			{
				Email = string.Empty,
			};

			//act

			//assert
			user.NormalizedEmail.Should().Be(string.Empty);
		}

		[Theory]
		[InlineData("Agustina+++m")]
		[InlineData("lkjñljñkasfasd")]
		public void NormalizedEmailShouldThrowNormalizedEmailException(string email)
		{
			//arrange
			var user = new User
			{
				Email = email,
			};

			//act
			Action act = () =>
			{
				var normalizedEmail = user.NormalizedEmail;
			};

			//assert
			act.Should()
				.Throw<NormalizedEmailException>()
				.WithMessage(string.Format(ExceptionMessages.NormalizedEmail, user.Email));
		}

		[Theory]
		[InlineData("Agustina+++++++@gmail.com")]
		[InlineData("Agustina+@gmail.com")]
		[InlineData("Agustina+sdfgsdfg@gmail.com")]
		[InlineData("Agustina+++f++fdf@gmail.com")]
		public void NormalizedEmailShouldRemoveAllCharactersAfterPlus(string email)
		{
			//arrange
			var user = new User
			{
				Email = email,
			};

			//act

			//assert
			user.NormalizedEmail.Should().Be("Agustina@gmail.com");
		}
	}
}
