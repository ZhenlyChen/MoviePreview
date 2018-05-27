using System;

using MoviePreview.ViewModels;

using Windows.UI.Xaml.Controls;

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
    }
}
