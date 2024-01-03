using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FoodService.DAL.Interfaces;
using FoodService.Modellayer;

namespace FoodService.DAL
{
    public class AdminUserData : IAdminUserData
    {
        private readonly IServiceContext _context;

        public AdminUserData(IServiceContext context)
        {
            _context = context;
        }

        public async Task<AdminUser> GetByUsernameAsync(string username)
        {
            return await _context.AdminUsers
                                 .FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
        }

        public async Task<bool> CreateAdminUserAsync(AdminUser adminUser)
        {
            // Tjek for gyldigt input (kan udvides efter behov)
            if (string.IsNullOrWhiteSpace(adminUser.Username) || string.IsNullOrWhiteSpace(adminUser.PasswordHash))
            {
                throw new ArgumentException("Username and password are required.");
            }

            // Tjek om brugernavnet allerede er taget
            var existingUser = await GetByUsernameAsync(adminUser.Username);
            if (existingUser != null)
            {
                throw new InvalidOperationException("Username already exists.");
            }

            // Generer salt og hash password
            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            var hash = BCrypt.Net.BCrypt.HashPassword(adminUser.PasswordHash, salt);

            // Opdater adminUser objektet med den hashede password og salt
            adminUser.PasswordHash = hash;
            adminUser.Salt = salt;

            try
            {
                _context.AdminUsers.Add(adminUser);
                var created = await _context.SaveChangesAsync();
                return created > 0;
            }
            catch (Exception ex)
            {
                // Log fejlen og/eller håndter den passende
                throw new Exception("Failed to create user.", ex);
            }
        }

        // Implementer yderligere metoder efter behov...
    }
}
