using SRA.ApiRest.Models.DTOs.UserDTO;

namespace SRA.ApiRest.Repository.IRepository
{
    public interface IGoogleLoginRepository
    {
        Task<UserLoginResponseDTO?> LoginWithGoogleAsync(GoogleLoginDTO dto);
    }
}
