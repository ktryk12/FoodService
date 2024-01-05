using FoodService.BusinessLogic.ServiceInterface;
using FoodService.BusinessLogic;
using FoodService.DAL.Interfaces;
using FoodService.DAL;
using FoodService.Modellayer;
using FoodService.DTOs;
using FoodService.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using System.ComponentModel.DataAnnotations;


var builder = WebApplication.CreateBuilder(args);


builder.Logging.AddConsole();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Hent database connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Database connection string 'DefaultConnection' is not configured.");
}

// Konfigurer EF Core DbContext
builder.Services.AddDbContext<ServiceContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IServiceContext>(provider => provider.GetRequiredService<ServiceContext>());



// Add Data Managers to the container
builder.Services.AddScoped<IShopData, ShopDataManager>();
builder.Services.AddScoped<ISalesItemGroupData, SalesItemGroupDataManager>();
builder.Services.AddScoped<ISalesItemData, SalesItemDataManager>();
builder.Services.AddScoped<IOrderData, OrderManager>();
builder.Services.AddScoped<IOrderlineData, OrderlineDataManager>();
builder.Services.AddScoped<IIngredientSalesItemData, IngredientSalesItemDataManager>();
builder.Services.AddScoped<IIngredientOrderlineData, IngredientOrderlineDataManager>();
builder.Services.AddScoped<IIngredientData, IngredientDataManager>();
builder.Services.AddScoped<IDiscountData, DiscountDataManager>();
builder.Services.AddScoped<ICustomerGroupData, CustomerGroupDataManager>();
builder.Services.AddScoped<ISalesItemCompositionData, SalesItemCompositionDataManager>();
builder.Services.AddScoped<IAdminUserData, AdminUserData>();
builder.Services.AddScoped<IImageData, ImageData>();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    // Hent JWT indstillinger direkte fra konfigurationen
    var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
    };
});



// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAllowSpecificOrigins",
    builder =>
    {
        builder.WithOrigins("http://localhost:4200")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});




// Add Services to the container
builder.Services.AddScoped<IShopService, ShopService>();
builder.Services.AddScoped<ISalesItemService, SalesItemService>();
builder.Services.AddScoped<ISalesItemGroupService, SalesItemGroupService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderlineService, OrderlineService>();
builder.Services.AddScoped<IIngredientService, IngredientService>();
builder.Services.AddScoped<IIngredientSalesItemService, IngredientSalesItemService>();
builder.Services.AddScoped<IIngredientOrderlineService, IngredientOrderlineService>();
builder.Services.AddScoped<ISalesItemCompositionService, SalesItemCompositionService>();
builder.Services.AddScoped<IDiscountService, DiscountService>();
builder.Services.AddScoped<ICustomerGroupService, CustomerGroupService>();





// JWT Token Service
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IAdminService, AdminService>();




var app = builder.Build();



// Configure the HTTP request pipeline.
var logger = app.Services.GetRequiredService<ILogger<Program>>();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
}
// Enable CORS with the defined policy
app.UseCors("MyAllowSpecificOrigins");

app.UseHttpsRedirection();
var imagesAbsolutePath = Path.Combine(app.Environment.ContentRootPath, "Images");
Console.WriteLine($"Images Absolute Path: {imagesAbsolutePath}");


app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(imagesAbsolutePath),
    RequestPath = "/Images"
});
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();


