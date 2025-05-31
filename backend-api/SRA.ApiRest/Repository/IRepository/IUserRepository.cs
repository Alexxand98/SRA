using SRA.ApiRest.Models.DTOs.UserDTO;

namespace SRA.ApiRest.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<UserLoginResponseDTO> Login(UserLoginDTO dto);
        Task<UserLoginResponseDTO> LoginWithGoogleAsync(string tokenId);
    }
}
