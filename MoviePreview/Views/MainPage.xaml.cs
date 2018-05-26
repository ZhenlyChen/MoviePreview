using System;

using MoviePreview.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace MoviePreview.Views
{
    public sealed partial class MainPage : Page
    {
        private MainViewModel ViewModel {
            get {
                return DataContext as MainViewModel;
            }
        }

        public MainPage()
        {
            InitializeComponent();
            Loaded += MainPage_Loaded;
            //ImageBrush imageBrush = new ImageBrush
            //{
            //    ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Background.jpg", UriKind.Absolute))
            //};
            //ContentArea.Background = imageBrush;
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoadData();
        }
    }
}
