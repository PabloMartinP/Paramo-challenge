using Sat.Recruitment.Api.Exceptions;
using System;
using static Sat.Recruitment.Api.Common.Constants;
using static Sat.Recruitment.Api.Entity.Enums;

namespace Sat.Recruitment.Api.Entity.Model
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public UserType UserType { get; set; }
        public decimal Money { get; set; }
		public decimal Tip { get; set; }
        public decimal MoneyWithTip
		{
			get
			{
                return Money + Tip;
			}
		}

        public bool IsDuplicate(User user)
		{
            return user.NormalizedEmail == NormalizedEmail ||
                user.Phone == Phone ||
                (user.Name == Name && user.Address == Address);
        }

		public virtual string NormalizedEmail  {
			get
			{
				if (!string.IsNullOrWhiteSpace(Email))
                {
					try
					{
                        var splittedEmail = Email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

                        var plusIndex = splittedEmail[0].IndexOf("+", StringComparison.Ordinal);

                        splittedEmail[0] = plusIndex < 0 ? splittedEmail[0].Replace(".", "") : splittedEmail[0].Replace(".", "").Remove(plusIndex);

                        var normalizedEmail = string.Join("@", new string[] { splittedEmail[0], splittedEmail[1] });

                        return normalizedEmail;
                    }
					catch (Exception e)
					{
						throw new NormalizedEmailException(string.Format(ExceptionMessages.NormalizedEmail, Email), e);
					}
                    
				}
				else
				{
                    return string.Empty;
				}
            }
        }
    }
}
