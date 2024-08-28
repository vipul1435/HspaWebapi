using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using webApi.Dtos;
using webApi.Interfaces;
using webApi.Modals;

namespace webApi.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUnitOfWork global;
        private readonly IConfiguration configuration;

        public AccountController(IUnitOfWork global, IConfiguration configuration)
        {
            this.global = global;
            this.configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginReqDto loginReqDto)
        {
            User dbUser =  await global.UserRepository.Authentication(loginReqDto);
            if (dbUser == null)
            {
                throw new UnauthorizedAccessException("User Email or Password is wrong");
            }
            LoginResDto userRes = new()
            {
                Email = dbUser.Email,
                Token = CreateJWT(dbUser)
            };

            return Ok(userRes);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            if(await global.UserRepository.AlreadyRegistered(registerUserDto.Email))
            {
                return StatusCode(500, "User already exists");
            } else
            {
                global.UserRepository.Register(registerUserDto);
                await global.SaveAsync();
                return Ok("User registered");
                
            }
        }

        private string CreateJWT(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Appsettings:Key").Value));
            var claims = new Claim[]
            {
                new(ClaimTypes.Name,user.UserName),
                new(ClaimTypes.NameIdentifier,user.Id.ToString())
            };

            var signingCredentials = new SigningCredentials(
                    key, SecurityAlgorithms.HmacSha256Signature
                );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
