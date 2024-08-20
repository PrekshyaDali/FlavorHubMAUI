using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FlavorHub.Views.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlavorHub.ViewModel
{
    public partial class FirstSplashScreenViewModel: ObservableObject
    {
        public INavigation _Navigation {  get; set; }
        public FirstSplashScreenViewModel(INavigation navigation) {
            _Navigation = navigation;
        }
        [RelayCommand]
        public async void NavigateToLogin()
        {
            await Shell.Current.GoToAsync("Login");

        }
    }
}
