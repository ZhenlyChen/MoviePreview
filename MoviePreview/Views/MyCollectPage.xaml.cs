using System;
using System.Diagnostics;
using GalaSoft.MvvmLight.Views;
using MoviePreview.Models;
using MoviePreview.Services;
using MoviePreview.ViewModels;
using Windows.ApplicationModel.Resources;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace MoviePreview.Views
{
    public sealed partial class MyCollectPage : Page
    {
        private MyCollectViewModel ViewModel {
            get { return DataContext as MyCollectViewModel; }
        }

        private bool isGuessLikeServiceOpened = false;

        public MyCollectPage()
        {
            InitializeComponent();
            this.SizeChanged += SizeChanged_GuessGrid;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            ViewModel.SaveData();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            CollectionsView.IsEnabled = true;
            LoadToOther.Visibility = Visibility.Collapsed;

            string GuessLike = ApplicationData.Current.LocalSettings.Values["GuessLike"] as string;
            if (GuessLike == null || GuessLike == "true")
            {
                isGuessLikeServiceOpened = true;
                if (GuessLike == null)
                {
                    ApplicationData.Current.LocalSettings.Values["GuessLike"] = "true";
                }
                LoadGuessList.Visibility = Visibility.Visible;
                await ViewModel.SyncData();
                GuessView.ItemsSource = ViewModel.GuessLike;
                LoadGuessList.Visibility = Visibility.Collapsed;
            }
            else
            {
                isGuessLikeServiceOpened = false;
                LoadGuessList.Visibility = Visibility.Collapsed;
                GuessGrid.Height = 0;
                await ViewModel.SyncData();
            }
        }

        private void SizeChanged_GuessGrid(object sender, SizeChangedEventArgs e)
        {
            if (isGuessLikeServiceOpened)
            {
                if (e.NewSize.Height < 640)
                {
                    if (GuessGrid.Height != 0)
                    {
                        GuessGrid.Height = 0;
                    }
                }
                else
                {
                    if (GuessGrid.Height == 0)
                    {
                        GuessGridOpenStoryboard.Begin();
                    }
                }
            }
        }

        private void ChangeNoteButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            // FlyoutBase.GetAttachedFlyout(((e.OriginalSource as Button).Parent as StackPanel).Parent as FrameworkElement).Hide();
            var a = (((e.OriginalSource as Button).Parent as StackPanel).Children[0] as TextBox).Text;
            var item = ((e.OriginalSource as Button).DataContext as MovieItem);
            ViewModel.OnsItemChange(item.ID, a);
        }

        private async void DisplayDeleteFileDialog()
        {
            ContentDialog deleteFileDialog = new ContentDialog
            {
                Title = ResourceLoader.GetForCurrentView().GetString("MyCollectPage_CS_Delete/Title"),
                Content = string.Format(ResourceLoader.GetForCurrentView().GetString("MyCollectPage_CS_Delete/Content"), ToDelItem.TitleCn),
                PrimaryButtonText = ResourceLoader.GetForCurrentView().GetString("MyCollectPage_CS_Delete/PrimaryButtonText"),
                CloseButtonText = ResourceLoader.GetForCurrentView().GetString("MyCollectPage_CS_Delete/CloseButtonText")
            };

            ContentDialogResult result = await deleteFileDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                ViewModel.OnsItemDelete(ToDelItem);
            }
        }

        private MovieItem ToDelItem;
        private void DeleteButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ToDelItem = (e.OriginalSource as Button).DataContext as MovieItem;
            DisplayDeleteFileDialog();
        }

        private async void CollectionsView_ItemClick(object sender, ItemClickEventArgs e)
        {
            CollectionsView.IsEnabled = false;
            LoadToOther.Visibility = Visibility.Visible;
            MovieItemDetail data;
            string movieId = (e.ClickedItem as MovieItem).ID;
            if (TimeAPIService.GetedDetail != null && TimeAPIService.GetedDetail.ContainsKey(movieId))
            {
                data = TimeAPIService.GetedDetail[movieId];
            }
            else
            {
                data = await TimeAPIService.GetMovieDetail(movieId);
            }
            CollectionsView.PrepareConnectedAnimation("Image", e.ClickedItem as MovieItem, "ImageMovie");
            NavigationService.Navigate(typeof(MovieDetailViewModel).FullName, data, new SuppressNavigationTransitionInfo());
        }

        private async void GuessView_ItemClick(object sender, ItemClickEventArgs e)
        {
            GuessView.IsEnabled = false;
            LoadGuessList.Visibility = Visibility.Visible;
            MovieItemDetail data;
            string movieId = (e.ClickedItem as MovieItem).ID;
            if (TimeAPIService.GetedDetail != null && TimeAPIService.GetedDetail.ContainsKey(movieId))
            {
                data = TimeAPIService.GetedDetail[movieId];
            }
            else
            {
                data = await TimeAPIService.GetMovieDetail(movieId);
            }
            GuessView.PrepareConnectedAnimation("Image", e.ClickedItem as MovieItem, "ImageMovie");
            NavigationService.Navigate(typeof(MovieDetailViewModel).FullName, data, new SuppressNavigationTransitionInfo());
        }

        private async void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog deleteFileDialog = new ContentDialog
            {
                Title = ResourceLoader.GetForCurrentView().GetString("MyCollectPage_CS_CloseGuessLikeServices/Title"),
                Content = ResourceLoader.GetForCurrentView().GetString("MyCollectPage_CS_CloseGuessLikeServices/Content"),
                PrimaryButtonText = ResourceLoader.GetForCurrentView().GetString("MyCollectPage_CS_CloseGuessLikeServices/PrimaryButtonText"),
                CloseButtonText = ResourceLoader.GetForCurrentView().GetString("MyCollectPage_CS_CloseGuessLikeServices/CloseButtonText")
            };
            ContentDialogResult result = await deleteFileDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                ApplicationData.Current.LocalSettings.Values["GuessLike"] = "false";
                isGuessLikeServiceOpened = false;
                GuessGridCloseStoryboard.Begin();
            }
        }

        public NavigationServiceEx NavigationService {
            get {
                return CommonServiceLocator.ServiceLocator.Current.GetInstance<NavigationServiceEx>();
            }
        }
    }
}
