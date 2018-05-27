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
                ImageSource = ViewModel.MovieDetail.Images[0].ImageUri,
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
                MovieImage.Source = TimeAPIService.CurrentDetail.ImageUri;
                await ViewModel.LoadAnimationAsync();
            }
            else
            {
                var data = e.Parameter as MovieItemDetail;
                if (data == null)
                {
                    data = JsonConvert.DeserializeObject<MovieItemDetail>((e.Parameter as JObject).ToString());
                }
                AddData(data);
                MovieImage.Source = new BitmapImage(new Uri(data.Image));
                ConnectedAnimation imageAnimation = ConnectedAnimationService.GetForCurrentView().GetAnimation("Image");
                if (imageAnimation != null)
                {
                    MovieImage.Opacity = 0;
                    MovieImage.ImageOpened += (sender_, e_) =>
                    {
                        MovieImage.Opacity = 1;
                        imageAnimation.TryStart(MovieImage);
                    };
                }
            }
            Singleton<SuspendAndResumeService>.Instance.OnBackgroundEntering += Instance_OnBackgroundEntering;
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
        }
    }
}
