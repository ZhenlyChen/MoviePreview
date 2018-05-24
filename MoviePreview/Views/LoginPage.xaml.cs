using System;

using MoviePreview.ViewModels;

using Windows.UI.Xaml.Controls;

namespace MoviePreview.Views
{
    public sealed partial class LoginPage : Page
    {
        private LoginViewModel ViewModel
        {
            get { return DataContext as LoginViewModel; }
        }

        public LoginPage()
        {
            InitializeComponent();
        }
    }
}
