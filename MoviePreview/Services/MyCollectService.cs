using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Uwp.Notifications;
using MoviePreview.Helpers;
using MoviePreview.Models;
using Windows.Storage;

namespace MoviePreview.Services
{
    public class MyCollectService
    {
        public List<MovieItem> Collections;
        public Dictionary<string, double> History;

        public MyCollectService()
        {
            Collections = new List<MovieItem>();
            // LoadFormStorage();
        }

        public async void LoadFormStorage()
        {
            // Collection
            var c = await ApplicationData.Current.LocalFolder.ReadAsync<List<MovieItem>>("MyCollection");
            if (c != null && c.Count != 0)
            {
                Collections = c;
            }
            else
            {
                Collections = new List<MovieItem>();
            }

            // History
            var d = await ApplicationData.Current.LocalFolder.ReadAsync<Dictionary<string, double>>("MyHistory");
            if (d != null && d.Count != 0)
            {
                History = d;
            }
            else
            {
                History = new Dictionary<string, double>();
            }

            // Toast
            if (ApplicationData.Current.LocalSettings.Values["Notice"] as string == "false")
            {
                return;
            }

            foreach(MovieItem movie in Collections)
            {
                var theDate = DateTime.ParseExact(movie.Date, "yyyy-M-d",
                                  CultureInfo.InvariantCulture);
                if ((theDate - DateTime.Now).TotalDays <= 10 && (theDate - DateTime.Now).TotalDays > 0)
                {
                    Singleton<ToastNotificationsService>.Instance.ShowToastNotificationOfComingMovie(movie);
                    break;
                }
            }
        }

        public async void SaveToStorage(List<MovieItem> data = null)
        {
            if (data != null) Collections = data;
            await ApplicationData.Current.LocalFolder.SaveAsync("MyCollection", Collections);
        }

        public async void SaveToStorage(Dictionary<string, double> history)
        {
            if (history != null) History = history;
            await ApplicationData.Current.LocalFolder.SaveAsync("MyHistory", History);
        }
    }
}
