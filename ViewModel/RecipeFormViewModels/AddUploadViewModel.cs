using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FlavorHub.Models;
using FlavorHub.Models.RecipeFormModels;
using FlavorHub.Models.SQLiteModels;
using FlavorHub.Repositories.Interfaces;
using Microsoft.Maui.Storage;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FlavorHub.ViewModel.RecipeFormViewModels
{
    public partial class AddUploadViewModel : ObservableObject, IQueryAttributable
    {
        private const int MaxImageCount = 5;
        private const int MaxVideoCount = 1;

        [ObservableProperty]
        private ObservableCollection<AddUploadModel> _SelectedMediaFiles = new ObservableCollection<AddUploadModel>();

        public IAsyncRelayCommand SelectImagesCommand => new AsyncRelayCommand(SelectImagesAsync);
        public IAsyncRelayCommand SelectVideosCommand => new AsyncRelayCommand(SelectVideosAsync);
        public IAsyncRelayCommand CompleteCommand => new AsyncRelayCommand(SaveRecipeToDatabaseAsync);

        private readonly IRecipeRepository _RecipeRepository;
        private readonly IUserService _UserService;

        [ObservableProperty]
        private string? _Title;

        [ObservableProperty]
        private string? _Description;

        [ObservableProperty]
        private int _CookingTime;

        [ObservableProperty]
        private int _Servings;

        [ObservableProperty]
        private string? _DifficultyLevel;

        [ObservableProperty]
        private string? _IngredientsJson;

        [ObservableProperty]
        private string? _StepsJson;

        [ObservableProperty]
        private string? _ImageUrlsJson;

        [ObservableProperty]
        private string? _VideoUrlJson;

        [ObservableProperty]
        private Guid? _UserId;

        public AddUploadViewModel(IRecipeRepository recipeRepository, IUserService userService)
        {
            _RecipeRepository = recipeRepository;
            _UserService = userService;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            try
            {
                if (query.ContainsKey("RecipeData"))
                {
                    var recipe = query["RecipeData"] as Recipe;
                    if (recipe != null)
                    {
                        Title = recipe.Title;
                        Description = recipe.Description;
                        CookingTime = recipe.CookingTime;
                        Servings = recipe.Servings;
                        DifficultyLevel = recipe.DifficultyLevel;
                        IngredientsJson = recipe.IngredientsJson;
                        StepsJson = recipe.StepsJson;
                        ImageUrlsJson = recipe.ImageUrlsJson;
                        VideoUrlJson = recipe.VideoUrlJson;
                        LoadMediaFromJson(ImageUrlsJson, VideoUrlJson);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private async Task SelectImagesAsync()
        {
            if (SelectedMediaFiles.Any(f => f.FileType == "Video"))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "You can only select either images or a video, not both.", "OK");
                return;
            }

            if (SelectedMediaFiles.Count >= MaxImageCount)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"You can only select up to {MaxImageCount} images.", "OK");
                return;
            }

            var customImageTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.Android, new[] { "image/png", "image/jpeg" } }
            });

            var results = await FilePicker.PickMultipleAsync(new PickOptions
            {
                PickerTitle = "Select PNG or JPG Images",
                FileTypes = customImageTypes
            });

            if (results != null)
            {
                var remainingSlots = MaxImageCount - SelectedMediaFiles.Count;
                var filesToAdd = results.Take(remainingSlots);

                foreach (var result in filesToAdd)
                {
                    var mediaFile = new AddUploadModel
                    {
                        FilePath = result.FullPath,
                        FileName = result.FileName,
                        FileType = "Image",
                        DeleteCommand = new RelayCommand(() => DeleteMediaFile(result.FullPath))
                    };

                    SelectedMediaFiles.Add(mediaFile);
                }
            }
        }

        private async Task SelectVideosAsync()
        {
            if (SelectedMediaFiles.Any(f => f.FileType == "Image"))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "You can only select either images or a video, not both.", "OK");
                return;
            }

            if (_SelectedMediaFiles.Count >= MaxVideoCount)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"You can only select {MaxVideoCount} video.", "OK");
                return;
            }

            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Select Video",
                FileTypes = FilePickerFileType.Videos
            });

            if (result != null)
            {
                var fileInfo = new FileInfo(result.FullPath);
                const long maxFileSizeInBytes = 100 * 1024 * 1024; // 100 MB

                if (fileInfo.Length > maxFileSizeInBytes)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "The selected video exceeds the allowed size of 100 MB.", "OK");
                    return;
                }

                var mediaFile = new AddUploadModel
                {
                    FilePath = result.FullPath,
                    FileName = result.FileName,
                    FileType = "Video",
                    DeleteCommand = new RelayCommand(() => DeleteMediaFile(result.FullPath))
                };

                SelectedMediaFiles.Add(mediaFile);
            }
        }

        private void DeleteMediaFile(string filePath)
        {
            var mediaFile = SelectedMediaFiles.FirstOrDefault(f => f.FilePath == filePath);
            if (mediaFile != null)
            {
                SelectedMediaFiles.Remove(mediaFile);
            }
        }

        private void LoadMediaFromJson(string? imageJson, string? videoUrl)
        {
            if (!string.IsNullOrEmpty(imageJson))
            {
                var images = JsonConvert.DeserializeObject<ObservableCollection<string>>(imageJson);
                if (images != null)
                {
                    foreach (var imagePath in images)
                    {
                        SelectedMediaFiles.Add(new AddUploadModel
                        {
                            FilePath = imagePath,
                            FileType = "Image"
                        });
                    }
                }
            }

            if (!string.IsNullOrEmpty(videoUrl))
            {
                SelectedMediaFiles.Add(new AddUploadModel
                {
                    FilePath = videoUrl,
                    FileType = "Video"
                });
            }
        }

        private (string imagesJson, string videosJson) SerializeMediaFilesToJson()
        {
            var imagePaths = SelectedMediaFiles.Where(f => f.FileType == "Image").Select(f => f.FilePath).ToList();
            var videoPaths = SelectedMediaFiles.Where(f => f.FileType == "Video").Select(f => f.FilePath).ToList();

            var imagesJson = JsonConvert.SerializeObject(imagePaths);
            var videosJson = JsonConvert.SerializeObject(videoPaths);

            return (imagesJson, videosJson);
        }

        private async Task LoadUserId()
        {
           var userId = await _UserService.GetUserIdAsync();
            UserId = userId.Value;
        }

        public async Task SaveRecipeToDatabaseAsync()
        {
            try
            {
                var (imagesJson, videosJson) = SerializeMediaFilesToJson();
                // Log the JSON strings
                Console.WriteLine($"Images JSON: {imagesJson}");
                Console.WriteLine($"Videos JSON: {videosJson}");
                await LoadUserId();
                var recipe = new Recipe
                {
                    Title = Title,
                    Description = Description,
                    CookingTime = CookingTime,
                    Servings = Servings,
                    DifficultyLevel = DifficultyLevel,
                    IngredientsJson = IngredientsJson,
                    StepsJson = StepsJson,
                    ImageUrlsJson = imagesJson,
                    VideoUrlJson = videosJson,
                    UserId = UserId.Value
                };

                await _RecipeRepository.AddRecipeAsync(recipe);
                await Application.Current.MainPage.DisplayAlert("Success", "Recipe saved successfully!", "OK");
                await Shell.Current.GoToAsync("///AddRecipe");

                //using messenger to clear all the input fields in all form pages
                WeakReferenceMessenger.Default.Send(new ClearDataMessage());

                // Clear form after saving
                Title = null;
                Description = null;
                CookingTime = 0;
                Servings = 0;
                DifficultyLevel = null;
                IngredientsJson = null;
                StepsJson = null;
                SelectedMediaFiles.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to save the recipe. Please try again.", "OK");
            }
        }
     
    }
}
