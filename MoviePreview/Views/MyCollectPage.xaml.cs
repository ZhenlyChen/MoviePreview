using System;

using MoviePreview.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace MoviePreview.Views
{
    public sealed partial class MyCollectPage : Page
    {
        private MyCollectViewModel ViewModel
        {
            get { return DataContext as MyCollectViewModel; }
        }

        public MyCollectPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            ViewModel.SaveData();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.SyncData();
        }
    }
}
