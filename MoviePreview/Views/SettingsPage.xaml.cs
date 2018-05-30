using System;

using MoviePreview.ViewModels;
using Windows.Storage;
using Windows.System.UserProfile;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace MoviePreview.Views
{
    public sealed partial class SettingsPage : Page
    {
        private SettingsViewModel ViewModel {
            get {
                return DataContext as SettingsViewModel;
            }
        }

        public SettingsPage()
        {
            InitializeComponent();
            string lang = ApplicationData.Current.LocalSettings.Values["CurrentLanguage"] as string;
            if (lang != null)
            {
                var langs = GlobalizationPreferences.Languages;
                lang = (langs.Count > 0 ? langs[0] : Windows.Globalization.Language.CurrentInputMethodLanguageTag);
            }
            switch (lang)
            {
                case "zh-cn":
                    lang_zh_cn.IsSelected = true;
                    break;
                case "en_us":
                    lang_en_us.IsSelected = true;
                    break;
                default:
                    lang_zh_cn.IsSelected = true;
                    break;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.Initialize();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combo = (ComboBox)sender;
            var item = (ComboBoxItem)combo.SelectedItem;
            string lang = item.Content.ToString();
            switch (lang)
            {
                case "简体中文":
                    ApplicationData.Current.LocalSettings.Values["CurrentLanguage"] = "zh-cn";
                    break;
                case "English":
                    ApplicationData.Current.LocalSettings.Values["CurrentLanguage"] = "en-us";
                    break;
            }
        }
    }
}
