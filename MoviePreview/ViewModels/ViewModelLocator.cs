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
            Register<MasterDetailViewModel, MasterDetailPage>();
            Register<ChartViewModel, ChartPage>();
            Register<SettingsViewModel, SettingsPage>();
            Register<MediaPlayerViewModel, MediaPlayerPage>();
            Register<ImageGalleryViewModel, ImageGalleryPage>();
            Register<ImageGalleryDetailViewModel, ImageGalleryDetailPage>();
            Register<LoginViewModel, LoginPage>();
            Register<LoginWebViewViewModel, LoginWebViewPage>();
        }

        public LoginWebViewViewModel LoginWebViewViewModel => ServiceLocator.Current.GetInstance<LoginWebViewViewModel>();

        public LoginViewModel LoginViewModel => ServiceLocator.Current.GetInstance<LoginViewModel>();

        public ImageGalleryDetailViewModel ImageGalleryDetailViewModel => ServiceLocator.Current.GetInstance<ImageGalleryDetailViewModel>();

        public ImageGalleryViewModel ImageGalleryViewModel => ServiceLocator.Current.GetInstance<ImageGalleryViewModel>();

        // A Guid is generated as a unique key for each instance as reusing the same VM instance in multiple MediaPlayerElement instances can cause playback errors
        public MediaPlayerViewModel MediaPlayerViewModel => ServiceLocator.Current.GetInstance<MediaPlayerViewModel>(Guid.NewGuid().ToString());

        public SettingsViewModel SettingsViewModel => ServiceLocator.Current.GetInstance<SettingsViewModel>();

        public ChartViewModel ChartViewModel => ServiceLocator.Current.GetInstance<ChartViewModel>();

        public MasterDetailViewModel MasterDetailViewModel => ServiceLocator.Current.GetInstance<MasterDetailViewModel>();

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
