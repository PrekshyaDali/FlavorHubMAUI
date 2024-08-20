using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FlavorHub.Models.RecipeFormModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlavorHub.ViewModel.RecipeFormViewModels
{
    public partial class DirectionViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<DirectionModel> _Directions = new ObservableCollection<DirectionModel>();

        //commands
        public IAsyncRelayCommand AddDirectionCommand => new AsyncRelayCommand(AddDirectionsAsync);
        public IAsyncRelayCommand<DirectionModel> RemoveDirectionCommand => new AsyncRelayCommand<DirectionModel>(RemoveDirectionsAsync);
        public IAsyncRelayCommand NavigateToAddUploads => new AsyncRelayCommand(NavigateToAddUploadsPage);

        public async Task NavigateToAddUploadsPage()
        {
            await Shell.Current.GoToAsync("AddUploads");
        }
        public DirectionViewModel() { }

        //Methods
        private async Task AddDirectionsAsync()
        {
            _Directions.Add(new DirectionModel());
            await Task.CompletedTask; 
        }

        private async Task RemoveDirectionsAsync(DirectionModel direction)
        {
            _Directions.Remove(direction);
            await Task.CompletedTask; 
        }
    }
}
