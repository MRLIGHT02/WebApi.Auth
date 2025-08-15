using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApiAuthAspNetCore.Entities;
using WebApiAuthAspNetCore.Models;

namespace WebApiAuthAspNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthController : ControllerBase
    {
        private readonly User _user;

        public UserAuthController(User user)
        {
            _user = user;
        }

        [HttpPost]
        [Route("register")]
        public ActionResult<User> Register(UserDto request)
        {
            var hashedPassword = new PasswordHasher<User>().HashPassword(_user, request.Password);
            _user.Username = request.Username;
            _user.PasswordHash = hashedPassword;

            return Ok(_user);


        }
    }
}
