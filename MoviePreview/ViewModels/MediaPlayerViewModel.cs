using System;
using System.Collections.ObjectModel;

using GalaSoft.MvvmLight;

using Windows.Media.Core;
using Windows.Media.Playback;

namespace MoviePreview.ViewModels
{
    public class MediaPlayerViewModel : ViewModelBase
    {
        private const string DefaultPoster = "ms-appx:///Assets/background.jpg";

        private IMediaPlaybackSource _source;

        public IMediaPlaybackSource Source
        {
            get { return _source; }
            set { Set(ref _source, value); }
        }

        private string _posterSource;

        public string PosterSource
        {
            get { return _posterSource; }
            set { Set(ref _posterSource, value); }
        }

        public MediaPlayerViewModel()
        {
            PosterSource = DefaultPoster;
        }
    }
}
