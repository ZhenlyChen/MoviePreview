using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using MoviePreview.Helpers;
using MoviePreview.Models;
using MoviePreview.Services;
using Windows.ApplicationModel.Resources;

namespace MoviePreview.ViewModels
{
    public class BlankViewModel : ViewModelBase
    {
        public BlankViewModel()
        {
        }
        public ObservableCollection<MovieItemComing> MovieItems { get; private set; } = new ObservableCollection<MovieItemComing>();
        public Boolean EmptyItem {
            get {
                return MovieItems.Count == 0;
            }
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <returns></returns>
        public async Task LoadData()
        {
            if (MovieItems.Count == 0)
            {
                MovieItems.Clear();
                var data = await TimeAPIService.GetComingMovies();
                foreach (var movie in data)
                {
                    // 只添加具有封面的电影
                    if (movie.Image != "")
                    {
                        MovieItems.Add(movie);
                    }
                }
                // 刷新页面
                RaisePropertyChanged("EmptyItem");
                // 添加磁贴
                string title = ResourceLoader.GetForCurrentView().GetString("BlankPage_TextTitle/Text");
                string date = ResourceLoader.GetForCurrentView().GetString("MovieDetailPage_TextDateHelp/Text");
                string look = ResourceLoader.GetForCurrentView().GetString("BlankViewModel_Look/Text");
                Singleton<LiveTileService>.Instance.AddTileToQueue(title, string.Empty, date, MovieItems[0].Date, look, MovieItems[0]);
            }
        }
    }
}
