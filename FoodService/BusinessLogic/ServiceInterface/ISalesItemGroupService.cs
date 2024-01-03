using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using FoodService.DAL.Interfaces;

using FoodService.Modellayer;
using FoodService.DTOs;

namespace FoodService.BusinessLogic.ServiceInterface
{
    public interface ISalesItemGroupService
    {
        Task<SalesItemGroupDto> CreateSalesItemGroupAsync(SalesItemGroupDto salesItemGroupDto);
        Task<SalesItemGroupDto> GetSalesItemGroupByIdAsync(int id);
        Task<IEnumerable<SalesItemGroupDto>> GetAllSalesItemGroupsAsync();
        Task<bool> UpdateSalesItemGroupAsync(SalesItemGroupDto salesItemGroupDto);
        Task<bool> DeleteSalesItemGroupAsync(int id);

    }
}
