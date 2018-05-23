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
            if (MovieItems.Count == 0) {
                MovieItems.Clear();
                var data = await APIService.GetInTheaters("广州", 1, 10);
                List<MovieItem> movieList = data.Item1;
                foreach (var movie in movieList) {
                    MovieItems.Add(movie);
                }
            }
        }

    }
}
