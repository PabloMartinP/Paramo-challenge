using FluentValidation;
using static Sat.Recruitment.Api.Common.Constants.UserConstants;

namespace Sat.Recruitment.Api.Model.Users.Request.Validations
{
	public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
	{
		public CreateUserRequestValidator()
		{
			RuleFor(p => p.Name)
				.NotEmpty().WithMessage(ValidationMessages.PropertyRequired);
			RuleFor(p => p.Email)
				.NotEmpty().WithMessage(ValidationMessages.PropertyRequired)
				.EmailAddress().WithMessage(ValidationMessages.EmailValid);
			RuleFor(p => p.Address)
				.NotEmpty().WithMessage(ValidationMessages.PropertyRequired);
			RuleFor(p => p.Phone)
				.NotEmpty().WithMessage(ValidationMessages.PropertyRequired);
		}
	}
}
