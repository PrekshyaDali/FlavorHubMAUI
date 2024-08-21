using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FlavorHub.Models;
using FlavorHub.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlavorHub.ViewModel
{
    public partial class GalleryViewModel: ObservableObject
    {
        private readonly PexelsService _PexelsService;

        [ObservableProperty]
        private ObservableCollection<PhotoModel> _Photos;
        [ObservableProperty]
        private string _SearchQuery;
        [ObservableProperty]
        private bool _IsRefreshing;

        //commands
        public ICommand SearchFoodCommand { get; set; }
        public ICommand RefreshFoodCommand { get; set; }
        
        public GalleryViewModel(PexelsService pexelsService)
        {
            _PexelsService = pexelsService;
            _SearchQuery = string.Empty;
            SearchFoodCommand = new AsyncRelayCommand(SearchPhotosAsync);
            RefreshFoodCommand = new AsyncRelayCommand(RefreshPhotosAsync);
            Photos = new ObservableCollection<PhotoModel>();
            LoadDefaultPhotos();
        }

        //Loading default photos without quering
        private async void LoadDefaultPhotos()
        {
          var photos = await _PexelsService.SearchPhotosAsync("Food");
            Photos = new ObservableCollection<PhotoModel>(photos.Select(url => new PhotoModel { Url = url }));
        }

        public async Task SearchPhotosAsync()
        {
            var photoUrls = await _PexelsService.SearchPhotosAsync(SearchQuery);
            Photos = new ObservableCollection<PhotoModel>(photoUrls.Select(url => new PhotoModel { Url = url }));
        }

        public async Task RefreshPhotosAsync()
        {
            IsRefreshing = true;

            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                // Fetch and cache default photos if no search query is provided
                await _PexelsService.FetchAndCachePhotosAsync("Food");
            }
            else
            {
                // Fetch and cache new photos based on the search query
                await _PexelsService.FetchAndCachePhotosAsync(SearchQuery);
            }

            // Update the photos collection with the newly fetched photos
            Photos = new ObservableCollection<PhotoModel>(
                _PexelsService.GetShuffledCachedPhotos().Select(url => new PhotoModel { Url = url })
            );

            IsRefreshing = false;
        }

    }
}
