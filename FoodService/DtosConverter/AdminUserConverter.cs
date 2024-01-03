using FoodService.DTOs;
using FoodService.Modellayer;

namespace FoodService.Dto_sConverter
{
    public class AdminUserConverter
    {
        public static AdminUser ToEntity(AdminUserDto dto)
        {
            return new AdminUser
            {

                Username = dto.Username,
                PasswordHash = dto.Password,

            };
        }
        public static AdminUserDto ToDto(AdminUser entity)
        {
            return new AdminUserDto
            {
                Username = entity.Username,
                Password = entity.PasswordHash,
            };
        }
    }
}
