using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlavorHub.ViewModel
{
    public partial class LoadingPageViewModel
    {
      private readonly FirebaseAuthClient _FirebaseAuthClient;
      public LoadingPageViewModel(FirebaseAuthClient firebaseAuthClient)
        {
            _FirebaseAuthClient = firebaseAuthClient;
            //CheckUserLoginDetails();
        }
    }
}
