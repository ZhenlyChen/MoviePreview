using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoviePreview.Helpers;
using MoviePreview.Models;
using Windows.Storage;

namespace MoviePreview.Services
{
    public class MyCollectService
    {
        public List<MovieItem> Collections;

        public MyCollectService()
        {
            Collections = new List<MovieItem>();
            // LoadFormStorage();
        }

        public async void LoadFormStorage()
        {
            var c = await ApplicationData.Current.LocalFolder.ReadAsync<List<MovieItem>>("MyCollection");
            if (c != null)
            {
                Collections = c;
            } else
            {
                Collections = new List<MovieItem>();
            }
        }

        public async void SaveToStorage(List<MovieItem> data = null)
        {
            if (data != null) Collections = data;
            await ApplicationData.Current.LocalFolder.SaveAsync("MyCollection", Collections);
        }
    }
}
