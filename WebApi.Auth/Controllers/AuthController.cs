using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Auth.Entities;
using WebApi.Auth.Models;

namespace WebApi.Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private static User? _user = new();

        [HttpPost("register")]
        public ActionResult<User?> Register(UserDto request)
        {
            _user!.UserName= request.UserName;
            _user.PasswordHash = new PasswordHasher<User>().HashPassword(_user,request.Password);

            return Ok(_user);
        }

        [HttpPost("Login")]
        public ActionResult<User?> Login(UserDto request)
        {
            if (_user!.UserName != request.UserName)
            {
                return Unauthorized("Invailid UserName");
            }
            if(new PasswordHasher<User>().VerifyHashedPassword(_user, _user.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
            {
                               return Unauthorized("Invailid Password");
            }

            return Ok(_user);
        }
    }
}
