using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using MoviePreview.Models;
using MoviePreview.Services;
using Windows.UI.Xaml.Data;

namespace MoviePreview.ViewModels {
    public class MainViewModel : ViewModelBase {
        public MainViewModel() {
        }
        public ObservableCollection<MovieItem> MovieItems { get; private set; } = new ObservableCollection<MovieItem>();

        public async Task LoadData() {
            MovieItems.Clear();
            var data = await APIService.GetInTheaters("广州", 1, 10);
            List<MovieItem> movieList = data.Item1;
            foreach (var movie in movieList) {
                MovieItems.Add(movie);
            }
        }


        public class StringToUrlConverter : IValueConverter {
            // Define the Convert method to convert a DateTime value to 
            // a month string.
            public object Convert(object value, Type targetType,
                object parameter, string language) {
                // value is the data from the source object.
                string url = (string)value;
                return new Uri(url);
            }
            public object ConvertBack(object value, Type targetType,
                object parameter, string language) {
                throw new NotImplementedException();
            }
        }

    }
}
