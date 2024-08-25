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
        public Guid UserID { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? IngredientsJson { get; set; }
        public string? StepsJson { get; set; }  
        public int CookingTime { get; set; }
        public string? DifficultyLevel { get; set; }
        public int Servings { get; set; }
        public string ImageUrlsJson { get; set; }
        public DateTime CreatedDate { get; set; }
        public string VideoUrlJson { get; set; }

        [Ignore] 
        //this will tell SQLite to not map this property into a column in database table
        public List<string> Ingredients
        {
            get => string.IsNullOrEmpty(IngredientsJson) 
                ? new List<string>()
                : JsonSerializer.Deserialize<List<string>>(IngredientsJson) ?? new List<string>();

            set => IngredientsJson = JsonSerializer.Serialize(value);
        }

        [Ignore]
        public List<string> Steps
        {
            get => string.IsNullOrEmpty(StepsJson)
                ? new List<string>()
                : JsonSerializer.Deserialize<List<string>>(StepsJson) ?? new List<string>();
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
