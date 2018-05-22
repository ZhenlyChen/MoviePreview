using System;

using MoviePreview.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MoviePreview.Views {
    public sealed partial class MainPage : Page {
        private MainViewModel ViewModel {
            get {
                return DataContext as MainViewModel;
            }
        }

        public MainPage() {
            InitializeComponent();
            Loaded += MainPage_Loaded;
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e) {
            await ViewModel.LoadData();
        }
    }
}
