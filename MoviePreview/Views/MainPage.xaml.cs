using System;
using GalaSoft.MvvmLight.Command;
using MoviePreview.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using MoviePreview.Models;
using MoviePreview.Services;
using Windows.UI.Xaml.Navigation;

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

        public NavigationServiceEx NavigationService {
            get {
                return CommonServiceLocator.ServiceLocator.Current.GetInstance<NavigationServiceEx>();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            LocationMovieList.IsEnabled = true;
            LoadToOther.IsActive = false;
        }

        private async void LocationMovieList_ItemClick(object sender, ItemClickEventArgs e)
        {
            LocationMovieList.IsEnabled = false;
            LoadToOther.IsActive = true;
            MovieItemDetail data;
            string movieId = (e.ClickedItem as MovieItemNow).ID;
            if (TimeAPIService.GetedDetail != null && TimeAPIService.GetedDetail.ContainsKey(movieId))
            {
                data = TimeAPIService.GetedDetail[movieId];
            } else
            {
                data = await TimeAPIService.GetMovieDetail(movieId);
            }
            LocationMovieList.PrepareConnectedAnimation("Image", e.ClickedItem as MovieItemNow, "ImageMovie");
            NavigationService.Navigate(typeof(MovieDetailViewModel).FullName, data);
        }
    }
}
