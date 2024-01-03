using Xunit;
using FoodService.DAL.Interfaces;
using FoodService.Modellayer;
using FoodService.DAL;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class ShopDataManagerTests : IDisposable
{
    private readonly ServiceContext _context;
    private readonly ShopDataManager _shopDataManager;

    public ShopDataManagerTests()
    {
        var options = new DbContextOptionsBuilder<ServiceContext>()
            .UseSqlServer("Server=localhost;Database=FoodService;User Id=sa;Password=Sommer2023;TrustServerCertificate=true;")
            .Options;

        _context = new ServiceContext(options);
        _shopDataManager = new ShopDataManager(_context);
    }

    [Fact]
    public async Task CreateShopAsync_AddsShop_ReturnsShop()
    {
        // Arrange
        var newShop = new Shop
        {
            Name = "Test Shop",
            Location = "Test Location",
            Type = ShopType.Resturant
        };

        // Act
        var result = await _shopDataManager.CreateShopAsync(newShop);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test Shop", result.Name);
        Assert.Equal("Test Location", result.Location);
        Assert.Equal(ShopType.Resturant, result.Type);

        // Verificer i databasen
        var shopInDb = await _context.Shops.FindAsync(result.Id);
        Assert.NotNull(shopInDb);
    }

    public void Dispose()
    {
        // Ryd op i databasen her, hvis nødvendigt
        _context.Dispose();
    }



    [Fact]
    public async Task GetShopByIdAsync_ReturnsShop()
    {
        // Arrange
        Shop existingShop = await _context.Shops.FirstOrDefaultAsync();
        int shopId;

        if (existingShop == null)
        {
            // Opret en ny shop, hvis der ikke er nogen i databasen
            var newShop = new Shop
            {
                Name = "Original Navn",
                Location = "Original Location",
                Type = ShopType.Foodstand
            };
            await _context.AddAsync(newShop);
            await _context.SaveChangesAsync();
            shopId = newShop.Id;
        }
        else
        {
            // Brug ID'et fra den eksisterende shop
            shopId = existingShop.Id;
        }

        // Act
        var result = await _shopDataManager.GetShopByIdAsync(shopId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(shopId, result.Id);
        Assert.Equal(existingShop?.Name ?? "Original Navn", result.Name);
        Assert.Equal(existingShop?.Location ?? "Original Location", result.Location);
        Assert.Equal(existingShop?.Type ?? ShopType.Foodstand, result.Type);
    }


    [Fact]
    public async Task UpdateShopAsync_UpdatesShop_ReturnsTrue()
    {
        // Arrange
        Shop existingShop = await _context.Shops.FirstOrDefaultAsync();
        if (existingShop == null)
        {
            existingShop = new Shop
            {
                Name = "Original Navn",
                Location = "Original Location",
                Type = ShopType.Foodstand
            };
            await _context.AddAsync(existingShop);
            await _context.SaveChangesAsync();
        }

        // Ændringer til shoppen
        existingShop.Name = "Opdateret Navn";

        // Act
        var result = await _shopDataManager.UpdateShopAsync(existingShop);

        // Assert
        Assert.True(result);

        // Kontroller, at ændringerne er blevet gemt i databasen
        var updatedShop = await _context.Shops.FindAsync(existingShop.Id);
        Assert.NotNull(updatedShop);
        Assert.Equal("Opdateret Navn", updatedShop.Name);
    }


    [Fact]
    public async Task DeleteShopAsync_DeletesShop_ReturnsTrue()
    {
        // Arrange
        var shopId = 1; // Antag at denne shop findes i databasen

        // Act
        var result = await _shopDataManager.DeleteShopAsync(shopId);

        // Assert
        Assert.True(result);
        // Yderligere assertions kan være at kontrollere, om shoppen faktisk er blevet fjernet fra databasen
    }
}

// Lignende for AddSalesItemToShopAsync og RemoveSalesItemFromShopAsync
