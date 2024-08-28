using webApi.Dtos;
using webApi.Modals;

namespace webApi.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Authentication(LoginReqDto loginReqDto);

        void Register(RegisterUserDto registerUserDto);

        Task<bool> AlreadyRegistered(string email);
    }
}
