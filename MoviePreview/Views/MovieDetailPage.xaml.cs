using System;

using MoviePreview.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace MoviePreview.Views
{
    public sealed partial class MovieDetailPage : Page
    {
        private MovieDetailViewModel ViewModel
        {
            get { return DataContext as MovieDetailViewModel; }
        }

        public MovieDetailPage()
        {
            InitializeComponent();
            Loaded += MovieDetailPage_Loaded;
        }

        private async void MovieDetailPage_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoadData();
            ImageBrush imageBrush = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(ViewModel.MovieDetail.Images[0])),
                Stretch = Stretch.UniformToFill,
                AlignmentY = AlignmentY.Top,
            };
            TopPanel.Background = imageBrush;
            FrameImage.Navigate(typeof(ImageGalleryPage));
        }
    }
}
