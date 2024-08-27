using CommunityToolkit.Mvvm.ComponentModel;
using FlavorHub.Models.RecipeFormModels;
using FlavorHub.Models.SQLiteModels;
using FlavorHub.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace FlavorHub.ViewModel.RecipeFormViewModels
{
    public class RecipeViewModel : ObservableObject
    {
        private readonly Recipe _Recipe;
        private readonly IUserService _UserService;
        private readonly IUserRepository _UserRepository;
        private string? _UserName;
        private string? _ProfilePicture;

        public RecipeViewModel(Recipe recipe, IUserService userService, IUserRepository userRepository)
        {
            _Recipe = recipe;
            _UserService = userService;
            _UserRepository = userRepository;
        }

        public async Task InitializeAsync()
        {
            await LoadUserName();
            await LoadProfilePicture();
        }

        private async Task LoadUserName()
        {
            var user = await _UserRepository.GetUserByIdAsync(_Recipe.UserId);
            if (user != null)
            {
                UserName = user.UserName;
            }
        }
        private async Task LoadProfilePicture()
        {
            var user = await _UserRepository.GetUserByIdAsync(_Recipe.UserId);
            if (user != null)
            {
                ProfilePicture = user.ProfilePicture;
            }
        }
        public string Title => _Recipe.Title;

        public string Description => _Recipe.Description;

        public int CookingTime => _Recipe.CookingTime;

        public string DifficultyLevel => _Recipe.DifficultyLevel;

        public int Servings => _Recipe.Servings;

        public Guid RecipeId => _Recipe.RecipeId;

        public List<IngredientModel> Ingredients => _Recipe.Ingredients;
        public List<DirectionModel> Steps => _Recipe.Steps;
        public List<string> ImageUrls => _Recipe.ImageUrls;
        public List<string> VideoUrls => _Recipe.VideoUrl;
        public DateTime CreatedDate => _Recipe.CreatedDate;

        public string UserName
        {
            get => _UserName;
            private set => SetProperty(ref _UserName, value);
        }
        public string ProfilePicture
        {
            get => _ProfilePicture;
            private set => SetProperty(ref _ProfilePicture, value);
        }
        public string FirstImageUrl => _Recipe.ImageUrls.FirstOrDefault() ?? "dotnet_bot.png";
    }
}
