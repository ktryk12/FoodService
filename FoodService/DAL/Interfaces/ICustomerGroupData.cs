using FoodService.Modellayer;

namespace FoodService.DAL.Interfaces
{
    public interface ICustomerGroupData
    {
        Task<CustomerGroup?> GetCustomerGroupByIdAsync(int id);
        Task<List<CustomerGroup>> GetAllCustomerGroupsAsync();
        Task<int?> CreateCustomerGroupAsync(CustomerGroup customerGroup);
        Task<bool> UpdateCustomerGroupByIdAsync(CustomerGroup customerGroupToUpdate);
        Task<bool> DeleteCustomerGroupByIdAsync(int id);
    }

}
