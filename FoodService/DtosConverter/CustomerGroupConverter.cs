using FoodService.DTOs;
using FoodService.Modellayer;

namespace FoodService.Dto_sConverter
{
    public class CustomerGroupConverter
    {
        public static CustomerGroup ToEntity(CustomerGroupDto dto)
        {
            return new CustomerGroup
            {
                Id = dto.Id,
                Name = dto.Name,
            };
        }
        public static CustomerGroupDto ToDto(CustomerGroup entity)
        {
            return new CustomerGroupDto
            {
                Id = entity.Id,
                Name = entity.Name,
            };
        }
    }
}
