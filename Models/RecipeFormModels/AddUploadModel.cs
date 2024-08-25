using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlavorHub.Models.RecipeFormModels
{
    public  class AddUploadModel
    {
        public string FilePath { get; set; }  
        public string FileName { get; set; } 
        public string FileType { get; set; }
        public IRelayCommand DeleteCommand { get; set; }
    }
}
