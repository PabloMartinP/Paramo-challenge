using Sat.Recruitment.Api.Model;
using System.Collections.Generic;

namespace Sat.Recruitment.Api.Builders.Contracts
{
	public interface IResultBuilder
	{
		public Result Ok();
		public Result Errors(List<string> errors);
		public Result Errors(string error);
	}
}
