using GameAuditor.Models;
using GameAuditor.Models.ViewModels;
using GameAuditor.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace GameAuditor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthController(IConfiguration configuration, IUserService userService, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _configuration = configuration;
            _userService = userService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet("getme"), Authorize]
        public ActionResult<string> GetMe()
        {
            var UserName = _userService.GetMyName();
            return Ok(UserName);
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(LoginViewModel request)
        {
            var user = new User();
            user.Email = request.Email;
            user.UserName = request.UserName;

            var result = await _userManager.CreateAsync(user, request.Password);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginViewModel request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, false, false);

            if (!result.Succeeded) return Unauthorized("Wrong password");

            string token = CreateToken(user);

            return Ok(token);
        }
        [HttpPost("refreshtoken")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var userName = _userService.GetMyName();
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            var refreshToken = Request.Cookies["refreshToken"];

            if (user.RefreshToken != refreshToken)
            {
                return Unauthorized("Invalid refresh token");
            }
            else if (user.TokenExpires < DateTime.Now) 
            {
                return Unauthorized("Token expired");
            }

            string token = CreateToken(user);
            var newRefreshToken = GenerateRefreshToken();
            SetRefreshToken(user, newRefreshToken);

            return Ok(token);
        }
        /*
        [HttpPost("refreshtoken")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var userName = _userService.GetMyName();
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            var refreshToken = Request.Cookies["refreshToken"];

            if (!user.RefreshToken.Equals(refreshToken))
            {
                return Unauthorized("Invalid refresh token");
            }
            else if (user.TokenExpires < DateTime.Now)
            {
                return Unauthorized("Token expired");
            }

            string token = CreateToken(user);
            var newRefreshToken = GenerateRefreshToken();
            SetRefreshToken(user, newRefreshToken);

            return Ok(token);
        }
        */
        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }

        private void SetRefreshToken(User user, RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.Expires;
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
