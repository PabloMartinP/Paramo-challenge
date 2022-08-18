using System;

namespace Sat.Recruitment.Api.Exceptions
{
	public class NormalizedEmailException: Exception
	{
        public NormalizedEmailException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
