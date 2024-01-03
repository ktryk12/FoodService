using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using FoodService.DAL.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using FoodService.Modellayer;

namespace FoodService.DAL
{
    public class CustomerGroupDataManager : ICustomerGroupData
    {
        private readonly IServiceContext _context;


        public CustomerGroupDataManager(IServiceContext context)
        {
            _context = context;

        }

        public async Task<CustomerGroup?> GetCustomerGroupByIdAsync(int id)
        {
            return await _context.CustomerGroups.FirstOrDefaultAsync(cg => cg.Id == id);
        }

        public async Task<List<CustomerGroup>> GetAllCustomerGroupsAsync()
        {
            return await _context.CustomerGroups.ToListAsync();

        }

        public async Task<int?> CreateCustomerGroupAsync(CustomerGroup customerGroup)
        {
            try
            {
                await _context.CustomerGroups.AddAsync(customerGroup);
                await _context.SaveChangesAsync();
                return customerGroup.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<bool> UpdateCustomerGroupByIdAsync(CustomerGroup customerGroupToUpdate)
        {
            try
            {
                var existingGroup = await _context.CustomerGroups.FirstOrDefaultAsync(cg => cg.Id == customerGroupToUpdate.Id);
                if (existingGroup == null) return false;

                _context.Entry(existingGroup).CurrentValues.SetValues(customerGroupToUpdate);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public async Task<bool> DeleteCustomerGroupByIdAsync(int id)
        {
            try
            {
                var customerGroup = await _context.CustomerGroups.FirstOrDefaultAsync(cg => cg.Id == id);
                if (customerGroup == null) return false;

                _context.CustomerGroups.Remove(customerGroup);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}

