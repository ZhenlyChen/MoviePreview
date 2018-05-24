using System;
using MoviePreview.Services;
using MoviePreview.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace MoviePreview.Views
{
    public sealed partial class LoginWebViewPage : Page
    {
        private LoginWebViewViewModel ViewModel
        {
            get { return DataContext as LoginWebViewViewModel; }
        }

        public LoginWebViewPage()
        {
            InitializeComponent();
            ViewModel.Initialize(webView);
        }
    }
}
