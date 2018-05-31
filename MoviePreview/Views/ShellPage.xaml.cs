using System;
using System.Collections.Generic;
using System.Diagnostics;
using MoviePreview.Models;
using MoviePreview.Services;
using MoviePreview.ViewModels;

using Windows.Foundation.Metadata;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
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

        private NavigationServiceEx NavigationService {
            get {
                return CommonServiceLocator.ServiceLocator.Current.GetInstance<NavigationServiceEx>();
            }
        }

        private Dictionary<string, string> searchMap;

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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            LoadToOther.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private async void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                if (sender.Text.Length == 0)
                {
                    sender.ItemsSource = null;
                }
                else
                {
                    searchMap = await TimeAPIService.GetMoviesTitleList(sender.Text);
                    sender.ItemsSource = searchMap.Keys;
                }
            }
        }

        private async void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                //从提示框中选择某一项时触发
            }
            else
            {
                //用户在输入时敲回车或者点击右边按钮确认输入时触发
                if (searchMap.ContainsKey(args.QueryText))
                {
                    LoadToOther.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    MovieItemDetail data = await TimeAPIService.GetMovieDetail(searchMap[args.QueryText]);
                    LoadToOther.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    NavigationService.Navigate(typeof(MovieDetailViewModel).FullName, data, new SuppressNavigationTransitionInfo());
                }
                else
                {
                    searchMap = await TimeAPIService.GetMoviesTitleList(args.QueryText);
                    sender.ItemsSource = searchMap.Keys;
                }
            }
        }
    }
}
