using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MoviePreview.Helpers;
using MoviePreview.Models;
using MoviePreview.Services;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Animation;

namespace MoviePreview.ViewModels
{
    public class HotMovieViewModel : ViewModelBase
    {
        public HotMovieViewModel()
        {
            MovieItems = new ObservableCollection<MovieItemNow>();
        }

        private ObservableCollection<MovieItemNow> _movieItems;
        public ObservableCollection<MovieItemNow> MovieItems {
            get => _movieItems;
            set => Set(ref _movieItems, value);
        }

        public enum Sort
        {
            Default, Date, Rating
        }
        private Sort _sortedType = Sort.Default;
        public Sort SortedType {
            get => _sortedType;
            set => Set(ref _sortedType, value);
        }

        public Boolean EmptyItem {
            get {
                return MovieItems.Count == 0;
            }
        }

        public async Task LoadData()
        {
            if (MovieItems.Count == 0)
            {
                MovieItems.Clear();
                var data = await TimeAPIService.GetLocationMovies();
                if (data.Count == 0)
                {
                    return;
                }
                foreach (var movie in data)
                {
                    MovieItems.Add(movie);
                }
                RaisePropertyChanged("EmptyItem");

                // 添加磁贴
                string title = ResourceLoader.GetForCurrentView().GetString("HotMovieViewModel_Title/Text");
                string look = ResourceLoader.GetForCurrentView().GetString("HotMovieViewModel_Look/Text");
                Singleton<LiveTileService>.Instance.AddTileToQueue(title, MovieItems[0].TitleEn, look, MovieItems[0].WantedCount.ToString(), MovieItems[0].CommonSpecial, MovieItems[0]);
            }
        }

    }
}
