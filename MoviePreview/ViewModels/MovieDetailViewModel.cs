using System;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using MoviePreview.Models;
using MoviePreview.Services;
using Windows.UI.Xaml;

namespace MoviePreview.ViewModels
{
    public class MovieDetailViewModel : ViewModelBase
    {
        public MovieDetailViewModel()
        {
        }
        public MovieItemDetail MovieDetail = new MovieItemDetail();

        public Visibility GetVisibility {
            get {
                return MovieDetail.ID == null ? Visibility.Collapsed : Visibility.Visible;
            }
        }
        public async Task LoadData()
        {
            if (MovieDetail.ID == null)
            {
                // MovieDetail = await TimeAPIService.GetMovieDetail("224595");
                MovieDetail = await TimeAPIService.GetMovieDetail("224595");
                RaisePropertyChanged("MovieDetail");
                RaisePropertyChanged("GetVisibility");
            }
        }
    }
}
