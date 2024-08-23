﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlavorHub.ViewModel
{
    public partial class GalleryFullPageViewModel : ObservableObject
    {
        //    Queryable property will tell the page that it should recieve a parameter named PhotoUrl from thenavigation dictionary
        [ObservableProperty]
        private string _PhotoUrl;
        public ICommand SavePhotoCommand { get; set; }
        public GalleryFullPageViewModel()
        {
            SavePhotoCommand = new AsyncRelayCommand(PhotoDownload);
        }

        [RelayCommand]
        private async Task PhotoDownload()
        {

        }
    }
}
