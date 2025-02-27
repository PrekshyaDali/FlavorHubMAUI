﻿using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth.Repository;
using FlavorHub.Models.SQLiteModels;
using FlavorHub.Repositories.Interfaces;
using FlavorHub.ViewModel.RecipeFormViewModels;
using FlavorHub.Views.Controls;
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
        private bool _IsFavorite = false;

        [ObservableProperty]
        private string? _FavoriteIcon;

        public ICommand FavoriteCommand { get; set; }
        private readonly ICommentsRepository _CommentRepository;
        private readonly IUserService _UserService;
        private readonly IFavoritesRepository _FavoritesRepostory;
        private readonly Repositories.Interfaces.IUserRepository _UserRepository;

        [ObservableProperty]
        private ObservableCollection<Comments> _CommentCollection = new ObservableCollection<Comments>();
        public ICommand SaveCommentCommand { get; set; }

        public RecipeDetailPageViewModel(ICommentsRepository commentsRepository, IUserService userService, Repositories.Interfaces.IUserRepository userRepository, IFavoritesRepository favoritesRepository)
        {
            _CommentRepository = commentsRepository;
            _UserService = userService;
            _UserRepository = userRepository;
            _FavoritesRepostory = favoritesRepository;
            SaveCommentCommand = new AsyncRelayCommand(SaveComments);
            FavoriteCommand = new AsyncRelayCommand(ToggleFavorite);
        }

        public async Task ToggleFavorite()
        {
            if (SelectedRecipe == null)
            {
                Console.WriteLine("No recipe selected.");
                return;
            }

            var userIdString = await _UserService.GetUserIdAsync();
            if (userIdString != null)
            {
                var userId = userIdString.Value;
                var favorite = await _FavoritesRepostory.GetFavoriteByRecipeAndUserAsync(SelectedRecipe.RecipeId, userId);

                try
                {
                    if (favorite != null)
                    {
                        await _FavoritesRepostory.DeleteFavoritesByIdAsync(favorite);
                        IsFavorite = false;
                        FavoriteIcon = "/Icons/heart.png";
                        await Toast.Make("Removed from favorites", ToastDuration.Short).Show();
                        SelectedRecipe.FavoriteCount--;

                    }
                    else
                    {
                        var newFavorite = new Favorites
                        {
                            FavoritesId = Guid.NewGuid(),
                            RecipeId = SelectedRecipe.RecipeId,
                            UserId = userId,
                        };

                        IsFavorite = true;
                        FavoriteIcon = "/Icons/heart_filled.png";
                        await _FavoritesRepostory.AddFavoritesAsync(newFavorite);
                        await Toast.Make("Added to favorites", ToastDuration.Short).Show();
                        SelectedRecipe.FavoriteCount++;
                    }
                    OnPropertyChanged(nameof(FavoriteCount));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error toggling favorite status: {ex}");
                }

            }
            else
            {
                Console.WriteLine("User ID is invalid or missing.");
            }
        }
        public int FavoriteCount => SelectedRecipe?.FavoriteCount ?? 0;
        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("SelectedRecipe"))
            {
                SelectedRecipe = query["SelectedRecipe"] as RecipeViewModel;
                var userIdResult = await _UserService.GetUserIdAsync();
                if (userIdResult != null)
                {
                    var userId = userIdResult.Value;
                    var favorite = await _FavoritesRepostory.GetFavoriteByRecipeAndUserAsync(SelectedRecipe.RecipeId, userId);
                    if (favorite != null)
                    {
                        IsFavorite = true;
                        FavoriteIcon = "/Icons/heart_filled.png";

                    }
                    else
                    {
                        IsFavorite = false;
                        FavoriteIcon = "/Icons/heart.png";
                    }
                }
                OnPropertyChanged(nameof(SelectedRecipe));
                OnPropertyChanged(nameof(FavoriteCount));
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
                await Application.Current.MainPage.DisplayAlert("Success", "Comments added successfully", "ok");
                CommentText = string.Empty;
                await LoadComments();

            }
            catch (Exception ex)
            {
              await Application.Current.MainPage.DisplayAlert("Failed", "Failed adding comments", "ok");
                Console.WriteLine(ex.ToString());      
            }
        }

        public async Task LoadComments()
        { 
          if(SelectedRecipe == null) 
          {
             return;
          }
            _CommentCollection.Clear();
            var comments = await _CommentRepository.GetCommentsByRecipeIdAsync(SelectedRecipe.RecipeId);
            if (comments != null && comments.Any())
            {
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
