using JWTAuthWebApi.Services;
using JWTAuthWebApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthWebApi.Controllers
{
    [CustomAuthorizeAttribute]
    [ApiVersion("1")]
    [ApiController]

    [Route("api/[controller]/{v:apiVersion}/[action]")]
    public class UserDetailsController : ControllerBase
    {
       
        private IUserService _userService;
        public UserDetailsController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
    }
}
