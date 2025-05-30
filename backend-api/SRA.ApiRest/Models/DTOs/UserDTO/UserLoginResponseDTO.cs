namespace SRA.ApiRest.Models.DTOs.UserDTO
{
    public class UserLoginResponseDTO
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }
}
