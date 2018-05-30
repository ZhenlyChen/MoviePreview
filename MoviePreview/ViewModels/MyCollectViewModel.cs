using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
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

        public MyCollectViewModel()
        {
            SyncData();
            if(Collections.Count != 0)
            {
                var theDate = DateTime.ParseExact(Collections[0].Date, "yyyy-M-d",
                                  CultureInfo.InvariantCulture);
                string tips;
                string date;
                string detail;
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

        public void SyncData()
        {
            Collections = new ObservableCollection<MovieItem>(Singleton<MyCollectService>.Instance.Collections);
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
    }
}
