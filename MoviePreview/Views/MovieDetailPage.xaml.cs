using System;

using MoviePreview.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using MoviePreview.Models;
using MoviePreview.Services;
using Windows.Foundation;
using Windows.System.Threading;
using Windows.UI.Core;
using MoviePreview.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.ApplicationModel.Resources;

namespace MoviePreview.Views
{
    public sealed partial class MovieDetailPage : Page
    {
        private MovieDetailViewModel ViewModel {
            get { return DataContext as MovieDetailViewModel; }
        }

        public MovieDetailPage()
        {
            InitializeComponent();
            ViewModel.Initialize(gridView);
        }

        private void AddData(MovieItemDetail m)
        {
            TimeAPIService.CurrentDetail = m;
            ViewModel.AddData();
            ChangeBG();
        }

        private void ChangeBG()
        {

            ImageBrush imageBrush = new ImageBrush
            {
                ImageSource = ImageCacheService.GetImage(ViewModel.MovieDetail.Images[0].Image),
                Stretch = Stretch.UniformToFill,
                AlignmentY = AlignmentY.Top,
            };
            TopPanel.Background = imageBrush;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.NavigationMode == NavigationMode.Back)
            {
                // 滚动到之前的地方
                ThreadPoolTimer DelayTimer = ThreadPoolTimer.CreateTimer(async (source) =>
                    {
                        await Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
                        {
                            var transform = gridView.TransformToVisual(ScrollPage);
                            var point = transform.TransformPoint(new Point(0, 0));
                            if (point.Y != 0)
                            {
                                var y = point.Y + ScrollPage.VerticalOffset;
                                if (y < 200) y += 200;
                                ScrollPage.ChangeView(null, y - 200, null, true);
                            }
                        });

                    }, TimeSpan.FromSeconds(0.1));
                ChangeBG();
                MovieImage.Source = ImageCacheService.GetImage(TimeAPIService.CurrentDetail.Image);
                await ViewModel.LoadAnimationAsync();
            }
            else
            {
                var data = e.Parameter as MovieItemDetail;
                if (data != null && data.ID == null)
                {
                    DisplayDeleteFileDialog();
                    return;
                }
                if (data == null)
                {
                    data = JsonConvert.DeserializeObject<MovieItemDetail>((e.Parameter as JObject).ToString());
                }
                AddData(data);
                MovieImage.Source = ImageCacheService.GetImage(data.Image); // new BitmapImage(new Uri(data.Image));
                ConnectedAnimation imageAnimation = ConnectedAnimationService.GetForCurrentView().GetAnimation("Image");
                if (imageAnimation != null)
                {
                    imageAnimation.TryStart(MovieImage);
                }
            }
            collectionButton();
            Singleton<SuspendAndResumeService>.Instance.OnBackgroundEntering += Instance_OnBackgroundEntering;
        }

        private async void DisplayDeleteFileDialog()
        {
            ContentDialog deleteFileDialog = new ContentDialog
            {
                Title = ResourceLoader.GetForCurrentView().GetString("NetErrorTitle/Text"),
                Content = ResourceLoader.GetForCurrentView().GetString("NetErrorBody/Text"),
                PrimaryButtonText = ResourceLoader.GetForCurrentView().GetString("NetErrorButton/Text"),
            };

            ContentDialogResult result = await deleteFileDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                if (NavigationService.CanGoBack)
                {
                    NavigationService.GoBack();
                }
                NavigationService.Navigate(typeof(MainViewModel).FullName, null, new SuppressNavigationTransitionInfo());
            }
        }

        public NavigationServiceEx NavigationService {
            get {
                return CommonServiceLocator.ServiceLocator.Current.GetInstance<NavigationServiceEx>();
            }
        }

        private MovieItem collectionItem;

        private void showAdd()
        {
            AppBarAdd.Visibility = Visibility.Visible;
            AppBarEdit.Visibility = Visibility.Collapsed;
        }
        private void showEdit()
        {
            AppBarAdd.Visibility = Visibility.Collapsed;
            AppBarEdit.Visibility = Visibility.Visible;
        }
        private void collectionButton()
        {
            showAdd();
            foreach (var i in Singleton<MyCollectService>.Instance.Collections)
            {
                if (i.ID == ViewModel.movieDetail.ID)
                {
                    showEdit();
                    collectionItem = i;
                    break;
                }
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            Singleton<SuspendAndResumeService>.Instance.OnBackgroundEntering -= Instance_OnBackgroundEntering;
        }

        private void Instance_OnBackgroundEntering(object sender, OnBackgroundEnteringEventArgs e)
        {
            ViewModel.SaveData(ref e);
        }



        private void BGRectangle_PointerEntered(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            EnterStoryboard.Begin();
        }

        private void BGRectangle_PointerExited(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            ExitStoryboard.Begin();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FlyoutAdd.Hide();
            ViewModel.AddFavorite(TextNote.Text, TimeAPIService.CurrentDetail);
            collectionButton();
        }

        // 修改备注
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            FlyoutEdit.Hide();
            ViewModel.AddFavorite(TextNoteEdit.Text, TimeAPIService.CurrentDetail);
            collectionButton();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            FlyoutEdit.Hide();
            ViewModel.DeleteFavorite(TimeAPIService.CurrentDetail);
            collectionButton();
        }

        private void AppBarEdit_Click(object sender, RoutedEventArgs e)
        {
            TextNoteEdit.Text = collectionItem.Note;
        }
    }
}
