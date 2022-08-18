using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Model;
using Sat.Recruitment.Api.Model.Users.Request;
using Sat.Recruitment.Api.Services.Contracts;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
	[ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
		private readonly IUserService userService;

		public UsersController(IUserService userService)
        {
			this.userService = userService;
		}

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Result>> CreateUser(CreateUserRequest request)
        {
            var result = await userService.CreateUser(request);
            
            if(result.IsSuccess)
                return Created("", result);
            
            return BadRequest(result.ErrorsToString());
        }
    }
    
}
