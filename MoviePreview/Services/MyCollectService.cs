using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoviePreview.Models;

namespace MoviePreview.Services
{
    public class MyCollectService
    {
        public List<MovieItem> Collections;

        public MyCollectService()
        {
            LoadFormStorage();
        }

        public void LoadFormStorage()
        {
            // TODO 从本地文件加载我的收藏
            // 使用 Helpers.SettingsStorageExtensions
        }

        public void SaveToStorage(List<MovieItem> data)
        {
            Collections = data;
            // TODO 把我的收藏存到本地文件中
            // 使用 Helpers.SettingsStorageExtensions
        }
    }
}
