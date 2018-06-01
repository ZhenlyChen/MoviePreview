using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MoviePreview.Helpers;
using MoviePreview.Models;
using MoviePreview.Services;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace MoviePreview.ViewModels
{
    public class MyCollectViewModel : ViewModelBase
    {
        private ObservableCollection<MovieItem> _collections;
        public ObservableCollection<MovieItem> Collections {
            get {
                if (_collections == null) _collections = new ObservableCollection<MovieItem>();
                return _collections;
            }
            set {
                Set(ref _collections, value);
            }
        }

        private ObservableCollection<MovieItem> _guessLike;
        public ObservableCollection<MovieItem> GuessLike {
            get {
                if (_guessLike == null) _guessLike = new ObservableCollection<MovieItem>();
                return _guessLike;
            }
            set {
                Set(ref _guessLike, value);
            }
        }

        // length = 41
        private static readonly string[] _movieType =
        {
            "剧情", "喜剧", "短片", "爱情", "纪录片", "动作", "惊悚", "恐怖", "动画", "犯罪",
            "冒险", "家庭", "悬疑", "奇幻", "科幻", "战争", "传记", "音乐", "历史", "歌舞",
            "西部", "运动", "古装", "武侠", "戏曲", "黑色电影", "儿童", "新闻", "真人秀", "舞台艺术",
            "成人", "脱口秀", "游戏秀", "情色", "文艺", "同性", "女性", "青春", "微电影", "灾难",
            "励志"
        };
        private Dictionary<string, double> _history;

        public MyCollectViewModel()
        {
            InitSync();
        }

        public async void InitSync()
        {
            await SyncData();
            if (Collections.Count != 0)
            {
                var theDate = DateTime.ParseExact(Collections[0].Date, "yyyy-M-d",
                                  CultureInfo.InvariantCulture);
                string tips;
                string date;
                int day = (int)(theDate - DateTime.Now).TotalDays;

                if (day <= 0)
                {
                    // tips => 正在上映
                    tips = ResourceLoader.GetForCurrentView().GetString("Shell_Main/Content");
                    date = string.Format(ResourceLoader.GetForCurrentView().GetString("MyCollectViewModel_Date1/Text"), -day);
                }
                else
                {
                    // tips => 即将上映
                    tips = ResourceLoader.GetForCurrentView().GetString("Shell_Blank/Content");
                    date = string.Format(ResourceLoader.GetForCurrentView().GetString("MyCollectViewModel_Date2/Text"), day);
                }
                string title = ResourceLoader.GetForCurrentView().GetString("Shell_MyCollect/Content");
                string look = ResourceLoader.GetForCurrentView().GetString("MyCollectViewModel_Look/Text");
                Singleton<LiveTileService>.Instance.AddTileToQueue(title, string.Empty, tips, date, look, Collections[0]);
            }
        }

        public void SaveData()
        {
            Singleton<MyCollectService>.Instance.SaveToStorage(Collections.ToList());
        }

        public void SaveHistory()
        {
            Singleton<MyCollectService>.Instance.SaveToStorage(_history);
        }

        public async Task SyncData()
        {
            Collections = new ObservableCollection<MovieItem>(Singleton<MyCollectService>.Instance.Collections);
            _history = new Dictionary<string, double>(Singleton<MyCollectService>.Instance.History);

            //获得即将上映电影进行筛选
            var data = await TimeAPIService.GetComingMovies();
            if (data.Count == 0)
            {
                return;
            }
            var list = new List<KeyValuePair<MovieItem, double>>();
            double normOfHistory2 = 0;
            foreach (var item in _history.Values)
            {
                normOfHistory2 += item * item;
            }
            foreach (var movie in data)
            {
                double normOfMovie2 = 0;
                double dotProduct = 0;
                string[] types = movie.MovieType.Split(new char[] { ' ' });
                foreach (string type in types)
                {
                    if (type.Length != 0 && _movieType.Contains(type))
                    {
                        normOfMovie2++;
                        if (_history.ContainsKey(type))
                        {
                            dotProduct += _history[type];
                        }
                    }
                }
                double result = dotProduct / (normOfMovie2 * normOfHistory2);
                list.Add(new KeyValuePair<MovieItem, double>(movie, result));
            }
            list.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));
            int count = 0;
            var guessCollection = new ObservableCollection<MovieItem>();
            foreach (var item in list)
            {
                // 只添加具有封面的电影
                if (item.Key.Image != "" && Collections.Where(i => i.ID == item.Key.ID).ToList().Count == 0)
                {
                    count++;
                    guessCollection.Add(item.Key);
                    if (count == 10)
                    {
                        break;
                    }
                }
            }
            GuessLike = guessCollection;
        }

        // 删除收藏
        public void OnsItemDelete(MovieItem item)
        {
            Collections.Remove(item);
            SaveData();
        }

        // 修改备注
        public void OnsItemChange(string id, string newNote)
        {
            // Nothing
        }

        // 添加历史记录
        public void HistoryAdd(string type)
        {
            if (_movieType.Contains(type))
            {
                Singleton<MyCollectService>.Instance.HistoryAdd(type);
            }
        }
    }
}
