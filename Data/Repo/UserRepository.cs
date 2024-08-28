using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;
using webApi.Dtos;
using webApi.Interfaces;
using webApi.Modals;

namespace webApi.Data.Repo
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext dc;

        public UserRepository(DataContext dc)
        {
            this.dc = dc;
        }

        public async Task<bool> AlreadyRegistered(string email)
        {
            return await dc.Users.AnyAsync(x => x.Email == email);
        }

        public async Task<User> Authentication(LoginReqDto loginReqDto)
        {
            User user = await dc.Users.FirstOrDefaultAsync(x => x.Email == loginReqDto.Email);
            if (user == null)
            {
                return null;
            }
            if (!MatchPassword(loginReqDto.Password, user.Password, user.PasswordKey))
            {
                return null;
            }
            return user;      
        }

        private bool MatchPassword(string password, byte[] userPassword, byte[] userPasswordKey)
        {

            using (HMACSHA512 hmac = new(userPasswordKey))
            {
                byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for(int i = 0; i < passwordHash.Length; i++)
                {
                    if (passwordHash[i] != userPassword[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public void Register(RegisterUserDto registerUserDto)
        {
            byte[] passwordHash, passwordKey;

            using (var hmac = new HMACSHA512())
            {
                passwordKey = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerUserDto.Password));
            }

            User user = new()
            {
                UserName = registerUserDto.UserName,
                Email = registerUserDto.Email,
                Password = passwordHash,
                PasswordKey = passwordKey

            };

            dc.Users.Add(user);
        }
    }
}
