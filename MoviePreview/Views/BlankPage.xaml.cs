using System;

using MoviePreview.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MoviePreview.Views
{
    public sealed partial class BlankPage : Page
    {
        private BlankViewModel ViewModel {
            get {
                return DataContext as BlankViewModel;
            }
        }

        public BlankPage()
        {
            InitializeComponent();
            Loaded += BlankPage_Loaded;
        }

        private async void BlankPage_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoadData();
        }
    }
}
