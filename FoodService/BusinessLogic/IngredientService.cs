using System.Collections.Generic;
using System.Threading.Tasks;
using FoodService.BusinessLogic.ServiceInterface;
using FoodService.DAL.Interfaces;
using FoodService.Dto_sConverter;
using FoodService.DTOs;
using FoodService.DtosConverter;
using FoodService.Modellayer;
using Microsoft.EntityFrameworkCore;


namespace FoodService.BusinessLogic
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientData _ingredientData;
        private readonly IImageData _imageData;
        private readonly IIngredientSalesItemData _ingredientSalesItemData;

        public IngredientService(IIngredientData ingredientData, IImageData imageData, IIngredientSalesItemData ingredientSalesItemData)
        {
            _ingredientData = ingredientData;
            _imageData = imageData;
            _ingredientSalesItemData = ingredientSalesItemData;
        }

        public async Task<IngredientDto> CreateIngredientAsync(CreateIngredientDto createIngredientDto)
        {
            var ingredient = CreateIngredientConverter.ToEntity(createIngredientDto);

            if (createIngredientDto.ImageFile != null)
            {
                ingredient.ImageUrl = await _imageData.SaveImageAsync(createIngredientDto.ImageFile);
            }
            else
            {
                ingredient.ImageUrl = "standardImageUrl.jpg"; // Standardbillede URL
            }

            var createdIngredient = await _ingredientData.CreateIngredientAsync(ingredient);

            if (createdIngredient != null)
            {
                return IngredientConverter.ToDto(createdIngredient); // Bruger IngredientConverter
            }
            else
            {
                // Håndter fejl her
                throw new Exception("Fejl ved oprettelse af Ingredient.");
            }
        }


        public async Task<bool> UpdateIngredientAsync(IngredientDto ingredientDto)
        {
            var ingredient = await _ingredientData.GetIngredientByIdAsync(ingredientDto.Id);
            if (ingredient == null)
            {
                return false;
            }

            ingredient = IngredientConverter.ToEntity(ingredientDto);

            // Handle image upload if included in DTO
            if (ingredientDto.ImageFile != null && ingredientDto.ImageFile.Length > 0)
            {
                // Delete old image if it exists
                if (!string.IsNullOrEmpty(ingredient.ImageUrl))
                {
                    await _imageData.DeleteImageAsync(ingredient.ImageUrl);
                }

                // Save new image and update ImageUrl
                ingredient.ImageUrl = await _imageData.SaveImageAsync(ingredientDto.ImageFile);
            }

            return await _ingredientData.UpdateIngredientAsync(ingredient);
        }

        public async Task<bool> DeleteIngredientAsync(int id)
        {
            return await _ingredientData.DeleteIngredientAsync(id);
        }

        public async Task<IngredientDto?> GetIngredientByIdAsync(int id)
        {
            var ingredient = await _ingredientData.GetIngredientByIdAsync(id);
            return ingredient == null ? null : IngredientConverter.ToDto(ingredient);
        }

        public async Task<List<IngredientDto>> GetAllIngredientsAsync()
        {
            // Antager at denne metode henter alle ingredienser.
            var ingredients = await _ingredientData.GetAllIngredientsAsync();

            // Brug IngredientConverter's ToDtoList metode for at konvertere listen af entiteter til en liste af DTO'er
            return IngredientConverter.ToDtoList(ingredients);
        }

        public async Task<IEnumerable<IngredientSalesItemDetailsDto>> GetIngredientsWithDetailsBySalesItemIdAsync(int salesItemId)
        {
            // Første skridt: Hent IngredientSalesItems for et bestemt SalesItem
            var ingredientSalesItems = await _ingredientSalesItemData.GetIngredientSalesItemsBySalesItemIdAsync(salesItemId);

            // Andet skridt: Hent de specifikke Ingredients baseret på deres ID'er
            var ingredientIds = ingredientSalesItems.Select(isi => isi.IngredientId).Distinct().ToList();
            var ingredients = await _ingredientData.GetIngredientsByIdAsync(ingredientIds);

            // Tredje skridt: Sammensæt DTO'er med både IngredientSalesItem og Ingredient data
            return ingredientSalesItems.Select(isi => new IngredientSalesItemDetailsDto
            {
                SalesItemId = isi.SalesItemId,
                IngredientId = isi.IngredientId,
                Min = isi.Min,
                Max = isi.Max,
                Count = isi.Count,
                Ingredient = ingredients.FirstOrDefault(i => i.Id == isi.IngredientId) != null ? IngredientConverter.ToDto(ingredients.FirstOrDefault(i => i.Id == isi.IngredientId)) : null
            });
        }


        // Methods related to image URL handling...
        // Implement these similar to above methods using DtoConverter.
    }
}

