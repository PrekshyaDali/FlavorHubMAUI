using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlavorHub.Models.RecipeFormModels
{
    public partial class DirectionModel: ObservableObject
    {
        [ObservableProperty]
        private string? _DirectionDescription;
    }
}
