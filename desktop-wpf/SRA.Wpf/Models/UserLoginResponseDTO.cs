using System.Collections.Generic;

namespace SRA.Wpf.Models
{
    public class UserLoginResponseDTO
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }
}
