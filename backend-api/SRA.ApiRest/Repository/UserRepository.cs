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
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _config;

        public UserRepository(UserManager<AppUser> userManager,
                               SignInManager<AppUser> signInManager,
                               IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }

        public async Task<UserLoginResponseDTO> Login(UserLoginDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return null!;

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded)
                return null!;

            return await GenerateTokenResponse(user);
        }

        public async Task<UserLoginResponseDTO> LoginWithGoogleAsync(string tokenId)
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(tokenId);

            if (!payload.Email.EndsWith("@iescomercio.com"))
                return null!;

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

            return await GenerateTokenResponse(user);
        }

        private async Task<UserLoginResponseDTO> GenerateTokenResponse(AppUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["ApiSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
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
