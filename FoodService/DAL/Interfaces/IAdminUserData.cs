using FoodService.Modellayer;

namespace FoodService.DAL.Interfaces
{
    public interface IAdminUserData
    {
        Task<AdminUser> GetByUsernameAsync(string username);
        Task<bool> CreateAdminUserAsync(AdminUser adminUser);
    }
}
