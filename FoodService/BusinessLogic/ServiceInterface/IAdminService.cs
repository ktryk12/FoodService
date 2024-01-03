using FoodService.DTOs;

namespace FoodService.BusinessLogic.ServiceInterface
{
    public interface IAdminService
    {


        Task<string> GenerateJwtToken(string username);
        Task<AdminUserDto> CreateAdminUserAsync(AdminUserDto adminUserDto);
        Task<bool> ValidateAdminCredentials(AdminUserDto adminUserDto);
    }
}
