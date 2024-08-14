using CommunityToolkit.Mvvm.Input;
using FlavorHub.Views.Authentication;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlavorHub.ViewModel
{
    public class RegisterViewModel
    {
        public ICommand NavigateToLoginPage {  get; set; }
        public INavigation _Navigation { get; set; }
        public RegisterViewModel(INavigation navigation)
        {
            _Navigation = navigation;
            NavigateToLoginPage = new RelayCommand(ToNavigateToLogin);
        }
        private async void ToNavigateToLogin()
        {
            if (_Navigation != null)
            {
                await _Navigation.PushAsync(new Login());
            }
        }

    }
}
