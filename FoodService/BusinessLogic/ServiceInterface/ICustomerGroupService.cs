using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FoodService.DTOs;
using FoodService.Modellayer;

namespace FoodService.BusinessLogic.ServiceInterface
{
    public interface ICustomerGroupService
    {
        Task<CustomerGroupDto> GetCustomerGroupByIdAsync(int id);
        Task<List<CustomerGroupDto>> GetAllCustomerGroupsAsync();
        Task<int> CreateCustomerGroupAsync(CustomerGroupDto customerGroupDto);
        Task<bool> UpdateCustomerGroupByIdAsync(CustomerGroupDto customerGroupDtoToUpdate);
        Task<bool> DeleteCustomerGroupByIdAsync(int id);

    }
}
