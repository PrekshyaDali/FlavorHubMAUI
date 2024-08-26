using FlavorHub.Models.RecipeFormModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FlavorHub.Models.SQLiteModels
{
    public class Recipe
    {
        [PrimaryKey, AutoIncrement]
        public Guid RecipeId { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? IngredientsJson { get; set; }
        public string? StepsJson { get; set; }  
        public int CookingTime { get; set; }
        public string? DifficultyLevel { get; set; }
        public int Servings { get; set; }
        public string ImageUrlsJson { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string VideoUrlJson { get; set; }

        //[Ignore] 
        ////this will tell SQLite to not map this property into a column in database table
        //public List<string> Ingredients
        //{
        //    get => string.IsNullOrEmpty(IngredientsJson) 
        //        ? new List<string>()
        //        : JsonSerializer.Deserialize<List<string>>(IngredientsJson) ?? new List<string>();

        //    set => IngredientsJson = JsonSerializer.Serialize(value);
        //}
        [Ignore]
        public List<IngredientModel> Ingredients
        {
            get
            {
                try
                {
                    return string.IsNullOrEmpty(IngredientsJson)
                        ? new List<IngredientModel>()
                        : JsonSerializer.Deserialize<List<IngredientModel>>(IngredientsJson) ?? new List<IngredientModel>();
                }
                catch (JsonException ex)
                {
                    // Log or handle deserialization error
                    Console.WriteLine($"Deserialization error: {ex.Message}");
                    return new List<IngredientModel>();
                }
            }
            set
            {
                try
                {
                    IngredientsJson = JsonSerializer.Serialize(value);
                }
                catch (JsonException ex)
                {
                    // Log or handle serialization error
                    Console.WriteLine($"Serialization error: {ex.Message}");
                }
            }
        }

        [Ignore]
        public List<DirectionModel> Steps
        {
            get => string.IsNullOrEmpty(StepsJson)
                ? new List<DirectionModel>()
                : JsonSerializer.Deserialize<List<DirectionModel>>(StepsJson) ?? new List<DirectionModel>();
            set => StepsJson = JsonSerializer.Serialize(value);
        }

        [Ignore]
        public List<string> ImageUrls
        {
            get => string.IsNullOrEmpty(ImageUrlsJson)
                   ? new List<string>()
                   : JsonSerializer.Deserialize<List<string>>(ImageUrlsJson) ?? new List<string>();
            set => ImageUrlsJson = JsonSerializer.Serialize(value);
        }

        [Ignore]
        public List<string> VideoUrl
        {
            get => string.IsNullOrEmpty(VideoUrlJson)
                   ? new List<string>()
                   : JsonSerializer.Deserialize<List<string>>(VideoUrlJson) ?? new List<string>();
            set => VideoUrlJson = JsonSerializer.Serialize(value);
        }
    }
}
