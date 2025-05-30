using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SRA.ApiRest.Models.DTOs.UserDTO;
using SRA.ApiRest.Models.Entity;
using SRA.ApiRest.Repository.IRepository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SRA.ApiRest.Repository
{
    public class GoogleLoginRepository : IGoogleLoginRepository
    {
        private readonly IConfiguration _config;
        private readonly UserManager<AppUser> _userManager;

        public GoogleLoginRepository(IConfiguration config, UserManager<AppUser> userManager)
        {
            _config = config;
            _userManager = userManager;
        }

        public async Task<UserLoginResponseDTO?> LoginWithGoogleAsync(GoogleLoginDTO dto)
        {
            GoogleJsonWebSignature.Payload payload;

            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new[] { _config["GoogleAuth:ClientId"] }
                };

                payload = await GoogleJsonWebSignature.ValidateAsync(dto.TokenId, settings);
            }
            catch
            {
                return null;
            }

            if (!payload.Email.EndsWith("@iescomercio.com"))
                return null;

            var user = await _userManager.FindByEmailAsync(payload.Email);
            if (user == null)
            {
                user = new AppUser
                {
                    Email = payload.Email,
                    UserName = payload.Email,
                    EmailConfirmed = true
                };

                await _userManager.CreateAsync(user);
                await _userManager.AddToRoleAsync(user, "Profesor");
            }

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["ApiSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                expires: DateTime.UtcNow.AddDays(7),
                claims: claims,
                signingCredentials: creds
            );

            return new UserLoginResponseDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Email = user.Email,
                Roles = roles.ToList()
            };
        }
    }
}