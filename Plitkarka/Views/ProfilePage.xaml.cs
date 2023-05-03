using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plitkarka.ViewModels;

namespace Plitkarka.Views;

public partial class ProfilePage : ContentPage
{
    public ProfilePage()
    {
        InitializeComponent();

        BindingContext = new ProfileViewModel();
    }
}