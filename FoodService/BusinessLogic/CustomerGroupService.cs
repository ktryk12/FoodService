using FoodService.DTOs;
using FoodService.DAL.Interfaces;
using FoodService.Dto_sConverter;
using FoodService.Modellayer;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoodService.BusinessLogic.ServiceInterface;

namespace FoodService.BusinessLogic
{
    public class CustomerGroupService : ICustomerGroupService
    {
        private readonly ICustomerGroupData _customerGroupData;

        public CustomerGroupService(ICustomerGroupData customerGroupData)
        {
            _customerGroupData = customerGroupData;
        }

        public async Task<CustomerGroupDto> GetCustomerGroupByIdAsync(int id)
        {
            var customerGroup = await _customerGroupData.GetCustomerGroupByIdAsync(id);
            return CustomerGroupConverter.ToDto(customerGroup);
        }

        public async Task<List<CustomerGroupDto>> GetAllCustomerGroupsAsync()
        {
            var customerGroups = await _customerGroupData.GetAllCustomerGroupsAsync();
            var customerGroupDtos = new List<CustomerGroupDto>();
            foreach (var customerGroup in customerGroups)
            {
                customerGroupDtos.Add(CustomerGroupConverter.ToDto(customerGroup));
            }
            return customerGroupDtos;
        }

        public async Task<int> CreateCustomerGroupAsync(CustomerGroupDto customerGroupDto)
        {
            var customerGroup = CustomerGroupConverter.ToEntity(customerGroupDto);
            return (int)await _customerGroupData.CreateCustomerGroupAsync(customerGroup);
        }

        public async Task<bool> UpdateCustomerGroupByIdAsync(CustomerGroupDto customerGroupDtoToUpdate)
        {
            var customerGroup = CustomerGroupConverter.ToEntity(customerGroupDtoToUpdate);
            return await _customerGroupData.UpdateCustomerGroupByIdAsync(customerGroup);
        }

        public async Task<bool> DeleteCustomerGroupByIdAsync(int id)
        {
            return await _customerGroupData.DeleteCustomerGroupByIdAsync(id);
        }
    }
}
