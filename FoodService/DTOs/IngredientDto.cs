using System;
using FoodService.Modellayer;
using System.Collections.Generic;

namespace FoodService.DTOs
{
    public class IngredientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal IngredientPrice { get; set; }
        public IFormFile ImageFile { get; set; }

        // Standard konstruktør
        public IngredientDto() { }

        // Konstruktør for oprettelse (uden Id)
        public IngredientDto(string name, decimal ingredientPrice, IFormFile imageFile)
        {
            Name = name;
            IngredientPrice = ingredientPrice;
            ImageFile = imageFile;
        }

        // Konstruktør for opdatering eller hentning 
        public IngredientDto(int id, string name, string imageUrl, decimal ingredientPrice)
        {
            Id = id;
             ImageUrl = imageUrl;
           Name = name;
            IngredientPrice = ingredientPrice;
        }
    }
}

