using System;

using MoviePreview.Services;
using MoviePreview.ViewModels;

using Windows.Foundation.Metadata;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace MoviePreview.Views
{
    public sealed partial class ShellPage : Page
    {
        private ShellViewModel ViewModel {
            get {
                return DataContext as ShellViewModel;
            }
        }

        public ShellPage()
        {
            InitializeComponent();
            HideNavViewBackButton();
            DataContext = ViewModel;
            ViewModel.Initialize(shellFrame, navigationView);
        }

        private void HideNavViewBackButton()
        {
            // 以下代码需要SDK版本17134，暂时禁用 使用老方法
            //if (ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 6)) {
            //    navigationView.IsBackButtonVisible = NavigationViewBackButtonVisible.Collapsed;
            //}
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
        }
    }
}
