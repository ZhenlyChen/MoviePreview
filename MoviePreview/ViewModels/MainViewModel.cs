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
using Windows.Foundation;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Animation;

namespace MoviePreview.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            MovieItems = new ObservableCollection<MovieItemNow>();
        }
        public ObservableCollection<MovieItemNow> _movieItems;
        public ObservableCollection<MovieItemNow> MovieItems {
            get => _movieItems;
            set => Set(ref _movieItems, value);
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
                foreach (var movie in data)
                {
                        MovieItems.Add(movie);
                }
                RaisePropertyChanged("EmptyItem");
                // TODO 加入磁贴 正在热映
                // Singleton<LiveTileService>.Instance.AddTileToQueue("最新上映", MovieItems[0].TitleCn, MovieItems[0].TitleEn);
            }
        }

    }
}
