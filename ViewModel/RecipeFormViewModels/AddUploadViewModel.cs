using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FlavorHub.Models.RecipeFormModels;
using Microsoft.Maui.Storage;
using System.Collections.ObjectModel;

namespace FlavorHub.ViewModel.RecipeFormViewModels
{
    public partial class AddUploadViewModel : ObservableObject
    {
        private const int MaxImageCount = 5;
        private const int MaxVideoCount = 1;

        [ObservableProperty]
        private ObservableCollection<AddUploadModel> _selectedMediaFiles = new ObservableCollection<AddUploadModel>();

        public IAsyncRelayCommand SelectImagesCommand => new AsyncRelayCommand(SelectImagesAsync);
        public IAsyncRelayCommand SelectVideosCommand => new AsyncRelayCommand(SelectVideosAsync);

        private async Task SelectImagesAsync()
        {
            // Ensure no video is selected before allowing image selection
            if (_selectedMediaFiles.Any(f => f.FileType == "Video"))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "You can only select either images or a video, not both.", "OK");
                return;
            }

            if (_selectedMediaFiles.Count >= MaxImageCount)
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
                // Limit the number of selected images to 5
                var remainingSlots = MaxImageCount - _selectedMediaFiles.Count;
                var filesToAdd = results.Take(remainingSlots);

                foreach (var result in filesToAdd)
                {
                    _selectedMediaFiles.Add(new AddUploadModel
                    {
                        FilePath = result.FullPath,
                        FileName = result.FileName,
                        FileType = "Image"
                    });
                }
            }
        }

        private async Task SelectVideosAsync()
        {
            // Ensure no images are selected before allowing video selection
            if (_selectedMediaFiles.Any(f => f.FileType == "Image"))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "You can only select either images or a video, not both.", "OK");
                return;
            }

            if (_selectedMediaFiles.Count >= MaxVideoCount)
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
                _selectedMediaFiles.Add(new AddUploadModel
                {
                    FilePath = result.FullPath,
                    FileName = result.FileName,
                    FileType = "Video"
                });
            }
        }
    }
}
