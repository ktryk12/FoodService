using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using FoodService.DTOs;
using FoodService.Modellayer;
using FoodService.DAL.Interfaces;
using FoodService.DAL;
using Microsoft.Extensions.Options;
using FoodService.Authentication;
using FoodService.BusinessLogic.ServiceInterface;

namespace FoodService.BusinessLogic
{
    public class AdminService : IAdminService
    {
        private readonly IAdminUserData _adminUserData;
        private readonly JwtSettings _jwtSettings;

        public AdminService(IAdminUserData adminUserData, IOptions<JwtSettings> jwtSettings)
        {
            _adminUserData = adminUserData ?? throw new ArgumentNullException(nameof(adminUserData));
            _jwtSettings = jwtSettings.Value ?? throw new ArgumentNullException(nameof(jwtSettings));
        }






        public async Task<AdminUserDto> CreateAdminUserAsync(AdminUserDto adminUserDto)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(adminUserDto.Password);

            var adminUser = new AdminUser
            {
                Username = adminUserDto.Username,
                PasswordHash = hash
            };

            var created = await _adminUserData.CreateAdminUserAsync(adminUser);
            if (!created)
            {
                throw new InvalidOperationException("Failed to create admin user.");
            }

            return new AdminUserDto { Username = adminUser.Username };
        }

        public async Task<bool> ValidateAdminCredentials(AdminUserDto adminUserDto)
        {
            var adminUser = await _adminUserData.GetByUsernameAsync(adminUserDto.Username);
            if (adminUser == null)
            {
                return false;
            }

            return BCrypt.Net.BCrypt.Verify(adminUserDto.Password, adminUser.PasswordHash);
        }
        public async Task<string> GenerateJwtToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            // Yderligere claims kan tilføjes her...
        };

            var token = new JwtSecurityToken(
        issuer: _jwtSettings.Issuer,      // Use the Issuer from JwtSettings
        audience: _jwtSettings.Audience,  // Use the Audience from JwtSettings
        claims: claims,
        expires: DateTime.Now.AddHours(1),
        signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
