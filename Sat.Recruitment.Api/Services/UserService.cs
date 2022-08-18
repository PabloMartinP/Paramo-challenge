using Sat.Recruitment.Api.Builders;
using Sat.Recruitment.Api.Builders.Contracts;
using Sat.Recruitment.Api.Entity.Model;
using Sat.Recruitment.Api.Entity.Repositories;
using Sat.Recruitment.Api.Model;
using Sat.Recruitment.Api.Model.Users.Request;
using Sat.Recruitment.Api.Model.Users.Request.Validations;
using Sat.Recruitment.Api.Services.CalculateTipUserStrategy.Contracts;
using Sat.Recruitment.Api.Services.Contracts;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using static Sat.Recruitment.Api.Common.Constants;

namespace Sat.Recruitment.Api.Services
{
	public class UserService: IUserService
    {
		private readonly ITipStrategyFactory tipStrategyFactory;
		private readonly IUserRepository userRepository;
		private readonly CreateUserRequestValidator createUserRequestValidator;
        private readonly IUserBuilder userBuilder;
        private readonly IResultBuilder resultBuilder;

        public UserService(
            ITipStrategyFactory tipStrategyFactory, 
            IUserRepository userRepository,
            CreateUserRequestValidator createUserRequestValidator,
            IResultBuilder resultBuilder,
            IUserBuilder userBuilder
            )
		{
            this.tipStrategyFactory = tipStrategyFactory;
            this.userRepository = userRepository;
			this.createUserRequestValidator = createUserRequestValidator;
            this.userBuilder = userBuilder;
            this.resultBuilder = resultBuilder;
        }

        private async Task<List<string>> ValidateCreateUser(CreateUserRequest request)
		{
            var validationResult = await createUserRequestValidator.ValidateAsync(request);

            if (!validationResult.IsValid )
                return validationResult.Errors.Select(p => p.ErrorMessage).ToList();

            List<string> errors = new List<string>();

            var newUser = userBuilder.BuildWith(request);

            var isDuplicated = await userRepository.Exists(p => p.IsDuplicate(newUser));

            if (isDuplicated)
            {
                errors.Add(UserConstants.ValidationMessages.UserDuplicated);
            }

            return errors;
        }
		public async Task<Result> CreateUser(CreateUserRequest request)
		{
            try
            {
                var errors = await ValidateCreateUser(request);

                if (errors.Any())
                    return resultBuilder.Errors(errors);

                var newUser = userBuilder.BuildWith(request);

                var calculateTip = tipStrategyFactory.GetTipStrategy(newUser.UserType);

                newUser.Tip = calculateTip.CalculateTip(newUser.Money);

                Debug.WriteLine("User Created");

                return resultBuilder.Ok();
            }
            catch (System.Exception e)
            {
                return resultBuilder.Errors("Unknow error");
            }
            
        }
    }
}
