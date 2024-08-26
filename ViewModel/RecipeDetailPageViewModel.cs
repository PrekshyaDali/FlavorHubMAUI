using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FlavorHub.Models.SQLiteModels;
using FlavorHub.Repositories.Interfaces;
using FlavorHub.ViewModel.RecipeFormViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlavorHub.ViewModel
{
    public partial class RecipeDetailPageViewModel : ObservableObject, IQueryAttributable 
    {

        [ObservableProperty]
        private RecipeViewModel _SelectedRecipe;

        [ObservableProperty]
        private string? _CommentText;

        [ObservableProperty]
        private Guid _RecipeId;

        [ObservableProperty]
        private Guid _UserId;

        private readonly ICommentsRepository _CommentRepository;

        private readonly IUserService _UserService;

        private ObservableCollection<Comments> _CommentCollection = new ObservableCollection<Comments>();
        public ICommand SaveCommentCommand { get; set; }

        public RecipeDetailPageViewModel(ICommentsRepository commentsRepository, IUserService userService)
        {
            LoadComments();
            SaveCommentCommand = new AsyncRelayCommand(SaveComments);
            _CommentRepository = commentsRepository;
            _UserService = userService;
        }

        public async Task SaveComments()
        {
            if (string.IsNullOrWhiteSpace(CommentText))
            {
                Application.Current.MainPage.DisplayAlert("Failed", "You cannot post empty comment", "ok");
                return;
            }
       
            try
            {
                var userId = await _UserService.GetUserIdAsync();
                var comments = new Comments
                {
                    CommentText = CommentText,
                    RecipeId = SelectedRecipe.RecipeId,
                    UserId = userId.Value,
                };
                Console.WriteLine($"UserId string: {userId}");
                Console.WriteLine($"RecipeId string: {SelectedRecipe.RecipeId}");
                await _CommentRepository.AddCommentAsync(comments);
                Application.Current.MainPage.DisplayAlert("Success", "Comments added successfully", "ok");
                CommentText = string.Empty;

            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Failed", "Failed adding comments", "ok");
                Console.WriteLine(ex.ToString());      
            }
        }

        public async Task LoadComments() { 
        
        
        }


        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("SelectedRecipe"))
            {
                SelectedRecipe = query["SelectedRecipe"] as RecipeViewModel;
                Console.WriteLine(SelectedRecipe.ImageUrls);
                Console.WriteLine(SelectedRecipe.Ingredients);
                Console.WriteLine("Ingredients Count: " + SelectedRecipe.Ingredients.Count);
            }
        }
    }
}
