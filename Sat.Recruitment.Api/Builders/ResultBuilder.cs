using Sat.Recruitment.Api.Builders.Contracts;
using Sat.Recruitment.Api.Model;
using System.Collections.Generic;

namespace Sat.Recruitment.Api.Builders
{
	public  class ResultBuilder: IResultBuilder
	{
		public  Result Ok()
		{
			return new Result()
			{
				IsSuccess = true
			};
		}

		public Result Errors(List<string> errors)
		{
			return new Result()
			{
				IsSuccess = false,
				Errors = errors
			};
		}

		public Result Errors(string error)
		{
			return Errors(
				new List<string>
					{
						error
					});
		}
	}
}
