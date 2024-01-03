using FoodService.DTOs;
using FoodService.Modellayer;
using FoodService.DAL.Interfaces;
using FoodService.Dto_sConverter;
using FoodService.BusinessLogic.ServiceInterface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodService.BusinessLogic
{
    public class SalesItemGroupService : ISalesItemGroupService
    {
        private readonly ISalesItemGroupData _salesItemGroupData;

        public SalesItemGroupService(ISalesItemGroupData salesItemGroupData)
        {
            _salesItemGroupData = salesItemGroupData;
        }

        public async Task<SalesItemGroupDto> CreateSalesItemGroupAsync(SalesItemGroupDto salesItemGroupDto)
        {
            var salesItemGroup = SalesItemGroupConverter.ToEntity(salesItemGroupDto);
            var createdSalesItemGroupId = await _salesItemGroupData.CreateSalesItemGroupAsync(salesItemGroup);

            if (createdSalesItemGroupId.HasValue)
            {
                var createdSalesItemGroup = await _salesItemGroupData.GetSalesItemGroupByIdAsync(createdSalesItemGroupId.Value);
                return SalesItemGroupConverter.ToDto(createdSalesItemGroup);
            }

            return null;
        }



        public async Task<SalesItemGroupDto> GetSalesItemGroupByIdAsync(int id)
        {
            var salesItemGroup = await _salesItemGroupData.GetSalesItemGroupByIdAsync(id);
            return salesItemGroup != null ? SalesItemGroupConverter.ToDto(salesItemGroup) : null;
        }


        public async Task<IEnumerable<SalesItemGroupDto>> GetAllSalesItemGroupsAsync()
        {
            var salesItemGroups = await _salesItemGroupData.GetAllSalesItemsGroupAsync();
            return salesItemGroups.Select(group => SalesItemGroupConverter.ToDto(group));
        }

        public async Task<bool> UpdateSalesItemGroupAsync(SalesItemGroupDto salesItemGroupDto)
        {
            var salesItemGroup = SalesItemGroupConverter.ToEntity(salesItemGroupDto);
            return await _salesItemGroupData.UpdateSalesItemGroupAsync(salesItemGroup);
        }

        public async Task<bool> DeleteSalesItemGroupAsync(int id)
        {
            return await _salesItemGroupData.DeleteSalesItemGroupAsync(id);
        }
    }
}


