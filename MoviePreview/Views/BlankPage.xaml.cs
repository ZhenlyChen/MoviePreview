using System;

using MoviePreview.ViewModels;

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
        }
    }
}
