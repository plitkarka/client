using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plitkarka.ViewModels;

namespace Plitkarka.Views;

public partial class ProfilePage : ContentPage
{
    private ProfileViewModel _viewModel;
    public ProfilePage(ProfileViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _viewModel.LoadPosts();
    }
}