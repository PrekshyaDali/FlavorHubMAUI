using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlavorHub.ViewModel
{
    public class LoginViewModel
    {
        public ICommand NavigateToRegisterCommand { get; set; }

        public LoginViewModel()
        {
            NavigateToRegisterCommand = new RelayCommand(NavigateToRegister);
        }
        private async void NavigateToRegister()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new Register());
        }

    }
}
