using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using webApi.Dtos;
using webApi.Errors;
using webApi.Extensions;
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
            ApiError apiError;

            if(loginReqDto.Email.IsEmpty() || loginReqDto.Password.IsEmpty())
            {
                apiError = new(BadRequest().StatusCode, "User email or password can't be empty.");
                return BadRequest(apiError);
            }
            User dbUser =  await global.UserRepository.Authentication(loginReqDto);
            if (dbUser == null)
            {
                apiError = new(Unauthorized().StatusCode, "Invalid user email or password" );
                return Unauthorized(apiError);
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
            ApiError apiError;
            if(await global.UserRepository.AlreadyRegistered(registerUserDto.Email))
            {

                apiError = new(BadRequest().StatusCode, "User already exists");
                return BadRequest(apiError);
            } 

            global.UserRepository.Register(registerUserDto);
            await global.SaveAsync();
            LoginReqDto loginReqDto = new()
            {
                Email = registerUserDto.Email,
                Password = registerUserDto.Password
            };
            User dbUser = await global.UserRepository.Authentication(loginReqDto);
            LoginResDto userRes = new()
            {
                Email = dbUser.Email,
                Token = CreateJWT(dbUser)
            };

            return Ok(userRes);
            
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
