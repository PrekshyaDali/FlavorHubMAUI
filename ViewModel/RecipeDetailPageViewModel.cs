using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth.Repository;
using FlavorHub.Models.SQLiteModels;
using FlavorHub.Repositories.Interfaces;
using FlavorHub.ViewModel.RecipeFormViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
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

        [ObservableProperty]
        private string? _IndividualComments;

        [ObservableProperty]
        private string? _CommentUserUserName;

        [ObservableProperty]
        private string? _CommentUserProfile;


        private readonly ICommentsRepository _CommentRepository;

        private readonly IUserService _UserService;
        private readonly Repositories.Interfaces.IUserRepository _UserRepository;


        [ObservableProperty]
        private ObservableCollection<Comments> _CommentCollection = new ObservableCollection<Comments>();
        public ICommand SaveCommentCommand { get; set; }

        public RecipeDetailPageViewModel(ICommentsRepository commentsRepository, IUserService userService, Repositories.Interfaces.IUserRepository userRepository)
        {
            SaveCommentCommand = new AsyncRelayCommand(SaveComments);
            _CommentRepository = commentsRepository;
            _UserService = userService;
            _UserRepository = userRepository;

        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("SelectedRecipe"))
            {
                SelectedRecipe = query["SelectedRecipe"] as RecipeViewModel;

                if (SelectedRecipe != null)
                {
                    LoadComments(); 
                }
            }
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
                await LoadComments();

            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Failed", "Failed adding comments", "ok");
                Console.WriteLine(ex.ToString());      
            }
        }

        public async Task LoadComments()
        { 
          if(SelectedRecipe == null) 
          {
             return;
          }  
            var comments = await _CommentRepository.GetCommentsByRecipeIdAsync(SelectedRecipe.RecipeId);
            if (comments != null && comments.Any())
            {
                _CommentCollection.Clear();
                foreach (var comment in comments)
                {
                    var user = await _UserRepository.GetUserByIdAsync(comment.UserId);
                    if (user != null) 
                    {
                     comment.UserName = user.UserName;
                     comment.UserProfileImage = user.ProfilePicture;
                    }
                    _CommentCollection.Add(comment);
                }
            }
        }
    }
}
