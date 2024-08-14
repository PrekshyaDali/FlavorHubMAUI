using CommunityToolkit.Mvvm.Input;
using FlavorHub.Views.Authentication;
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
        public RegisterViewModel()
        {
            NavigateToLoginPage = new RelayCommand(ToNavigateToLogin);
        }
        private async void ToNavigateToLogin()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new Login());
        }

    }
}
