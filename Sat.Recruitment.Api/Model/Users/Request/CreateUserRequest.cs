using System.ComponentModel.DataAnnotations;
using static Sat.Recruitment.Api.Entity.Enums;

namespace Sat.Recruitment.Api.Model.Users.Request
{
	public class CreateUserRequest
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }
		public string Phone { get; set; }
		public UserType UserType { get; set; }
		public decimal Money { get; set; }
	}
}
