﻿using System;
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
            // todo toast

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
            //toast
        }

        public async void SaveToStorage(List<MovieItem> data = null)
        {
            if (data != null) Collections = data;
            await ApplicationData.Current.LocalFolder.SaveAsync("MyCollection", Collections);
        }
    }
}
