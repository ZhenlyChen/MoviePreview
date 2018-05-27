using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MoviePreview.Helpers;
using MoviePreview.Models;
using MoviePreview.Services;
using Windows.UI.Xaml.Controls;

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
        }

        public void SaveData()
        {
            Singleton<MyCollectService>.Instance.SaveToStorage(Collections.ToList());
        }

        public void SyncData()
        {
            Collections = new ObservableCollection<MovieItem>(Singleton<MyCollectService>.Instance.Collections);
        }

        // 打开收藏详情
        private ICommand _itemSelectedCommand;
        public ICommand ItemSelectedCommand => _itemSelectedCommand ?? (_itemSelectedCommand = new RelayCommand<ItemClickEventArgs>(OnsItemSelected));
        private void OnsItemSelected(ItemClickEventArgs args)
        {
            // TODO 打开收藏详情
        }

        // 删除收藏
        private ICommand _itemDeleteCommand;
        public ICommand ItemDeleteCommand => _itemDeleteCommand ?? (_itemDeleteCommand = new RelayCommand<ItemClickEventArgs>(OnsItemDelete));
        private void OnsItemDelete(ItemClickEventArgs args)
        {
            // TODO 删除收藏
        }

        // 修改备注
        private ICommand _itemChangeCommand;
        public ICommand ItemChangeCommand => _itemChangeCommand ?? (_itemChangeCommand = new RelayCommand<ItemClickEventArgs>(OnsItemChange));
        private void OnsItemChange(ItemClickEventArgs args)
        {
            // TODO 修改备注
        }
    }
}
