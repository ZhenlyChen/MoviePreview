using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using MoviePreview.Helpers;
using MoviePreview.Models;
using MoviePreview.Services;

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
                if (data.Count == 0)
                {
                    return;
                }
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
                Singleton<LiveTileService>.Instance.AddTileToQueue("即将上映", "", "上映时间：", MovieItems[0].Date, "猛戳看档期", MovieItems[0]);
            }
        }
    }
}
