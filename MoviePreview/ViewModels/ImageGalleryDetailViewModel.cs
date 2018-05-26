using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using GalaSoft.MvvmLight;

using MoviePreview.Helpers;
using MoviePreview.Models;
using MoviePreview.Services;

using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace MoviePreview.ViewModels
{
    public class ImageGalleryDetailViewModel : ViewModelBase
    {
        private static UIElement _image;
        private object _selectedImage;
        private ObservableCollection<PostItem> _source;

        public object SelectedImage
        {
            get => _selectedImage;
            set
            {
                Set(ref _selectedImage, value);
                ApplicationData.Current.LocalSettings.SaveString(ImageGalleryViewModel.ImageGallerySelectedIdKey, ((PostItem)SelectedImage).ID);
            }
        }

        public ObservableCollection<PostItem> Source
        {
            get => _source;
            set => Set(ref _source, value);
        }

        public ImageGalleryDetailViewModel()
        {
        }

        public void InitSource()
        {
            Source = new ObservableCollection<PostItem>(TimeAPIService.CurrentDetail.Images);
        }

        public void SetImage(UIElement image) => _image = image;

        public async Task InitializeAsync(PostItem sampleImage, NavigationMode navigationMode)
        {
            InitSource();
            if (sampleImage != null && navigationMode == NavigationMode.New)
            {
                SelectedImage = Source.FirstOrDefault(i => i.ID == sampleImage.ID);
            }
            else
            {
                var selectedImageId = await ApplicationData.Current.LocalSettings.ReadAsync<string>(ImageGalleryViewModel.ImageGallerySelectedIdKey);
                if (!string.IsNullOrEmpty(selectedImageId))
                {
                    SelectedImage = Source.FirstOrDefault(i => i.ID == selectedImageId);
                }
            }

            var animation = ConnectedAnimationService.GetForCurrentView().GetAnimation(ImageGalleryViewModel.ImageGalleryAnimationOpen);
            animation?.TryStart(_image);
        }

        public void SetAnimation()
        {
            ConnectedAnimationService.GetForCurrentView()?.PrepareToAnimate(ImageGalleryViewModel.ImageGalleryAnimationClose, _image);
        }
    }
}
