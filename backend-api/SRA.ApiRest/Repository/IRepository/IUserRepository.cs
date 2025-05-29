using SRA.ApiRest.Models.DTOs.UserDTO;

namespace SRA.ApiRest.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<UserLoginResponseDto> Login(UserLoginDto dto);
    }
}
