using System;
using System.Diagnostics;
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

        private bool init = false;

        public SettingsPage()
        {
            InitializeComponent();
            string lang = ApplicationData.Current.LocalSettings.Values["CurrentLanguage"] as string;
            if (lang == null)
            {
                var langs = GlobalizationPreferences.Languages;
                lang = (langs.Count > 0 ? langs[0] : Windows.Globalization.Language.CurrentInputMethodLanguageTag);
            }
            switch (lang.ToLower())
            {
                case "zh-cn":
                    LangSelect.SelectedItem = lang_zh_cn;
                    break;
                case "en-us":
                    LangSelect.SelectedItem = lang_en_us;
                    break;
                default:
                    LangSelect.SelectedItem = lang_zh_cn;
                    break;
            }
            string Notice = ApplicationData.Current.LocalSettings.Values["Notice"] as string;
            if (Notice == null)
            {
                ViewModel.SwitchValue = true;
            } else
            {
                ViewModel.SwitchValue = Notice == "true" ? true : false;
            }

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.Initialize();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (init == false)
            {
                init = true;
                return;
            }
            var combo = (ComboBox)sender;
            if (combo.SelectedIndex == -1)
            {
                return;
            }
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

        private void NoticeSwitch_Toggled(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (NoticeSwitch.IsOn == true)
            {
                ApplicationData.Current.LocalSettings.Values["Notice"] = "true";
            } else
            {
                ApplicationData.Current.LocalSettings.Values["Notice"] = "false";
            }
        }
    }
}
