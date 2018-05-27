using System;

using CommonServiceLocator;

using GalaSoft.MvvmLight.Ioc;

using MoviePreview.Services;
using MoviePreview.Views;

namespace MoviePreview.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register(() => new NavigationServiceEx());
            SimpleIoc.Default.Register<ShellViewModel>();
            Register<MainViewModel, MainPage>();
            Register<BlankViewModel, BlankPage>();
            Register<SettingsViewModel, SettingsPage>();
            Register<MediaPlayerViewModel, MediaPlayerPage>();
            Register<ImageGalleryDetailViewModel, ImageGalleryDetailPage>();
            Register<LoginWebViewViewModel, LoginWebViewPage>();
            Register<MovieDetailViewModel, MovieDetailPage>();
            Register<MyCollectViewModel, MyCollectPage>();
        }

        public MyCollectViewModel MyCollectViewModel => ServiceLocator.Current.GetInstance<MyCollectViewModel>();

        public MovieDetailViewModel MovieDetailViewModel => ServiceLocator.Current.GetInstance<MovieDetailViewModel>();

        public LoginWebViewViewModel LoginWebViewViewModel => ServiceLocator.Current.GetInstance<LoginWebViewViewModel>();
        
        public ImageGalleryDetailViewModel ImageGalleryDetailViewModel => ServiceLocator.Current.GetInstance<ImageGalleryDetailViewModel>();
        
        // A Guid is generated as a unique key for each instance as reusing the same VM instance in multiple MediaPlayerElement instances can cause playback errors
        public MediaPlayerViewModel MediaPlayerViewModel => ServiceLocator.Current.GetInstance<MediaPlayerViewModel>(Guid.NewGuid().ToString());

        public SettingsViewModel SettingsViewModel => ServiceLocator.Current.GetInstance<SettingsViewModel>();

        public BlankViewModel BlankViewModel => ServiceLocator.Current.GetInstance<BlankViewModel>();

        public MainViewModel MainViewModel => ServiceLocator.Current.GetInstance<MainViewModel>();

        public ShellViewModel ShellViewModel => ServiceLocator.Current.GetInstance<ShellViewModel>();

        public NavigationServiceEx NavigationService => ServiceLocator.Current.GetInstance<NavigationServiceEx>();

        public void Register<VM, V>()
            where VM : class
        {
            SimpleIoc.Default.Register<VM>();

            NavigationService.Configure(typeof(VM).FullName, typeof(V));
        }
    }
}
