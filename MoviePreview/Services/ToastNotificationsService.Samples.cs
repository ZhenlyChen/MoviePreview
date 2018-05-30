using Microsoft.Toolkit.Uwp.Notifications;
using MoviePreview.Models;
using Windows.ApplicationModel.Resources;
using Windows.UI.Notifications;

namespace MoviePreview.Services
{
    internal partial class ToastNotificationsService
    {
        public void ShowToastNotificationSample()
        {
            // Create the toast content
            var content = new ToastContent()
            {
                // More about the Launch property at https://developer.microsoft.com/en-us/windows/uwp-community-toolkit/api/microsoft_toolkit_uwp_notifications_toastcontent
                Launch = "ToastContentActivationParams",

                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children =
                        {
                            new AdaptiveText()
                            {
                                Text = "Sample Toast Notification"
                            },

                            new AdaptiveText()
                            {
                                 Text = @"Click OK to see how activation from a toast notification can be handled in the ToastNotificationService."
                            }
                        }
                    }
                },

                Actions = new ToastActionsCustom()
                {
                    Buttons =
                    {
                        // More about Toast Buttons at https://developer.microsoft.com/en-us/windows/uwp-community-toolkit/api/microsoft_toolkit_uwp_notifications_toastbutton
                        new ToastButton("OK", "ToastButtonActivationArguments")
                        {
                            ActivationType = ToastActivationType.Foreground
                        },

                        new ToastButtonDismiss("Cancel")
                    }
                }
            };

            // Add the content to the toast
            var toast = new ToastNotification(content.GetXml())
            {
                Tag = "MoviePreviewToastAddToCollection"
            };

            // And show the toast
            ShowToastNotification(toast);
        }


        public void ShowToastNotificationOfComingMovie(MovieItem Movie)
        {
            string title = ResourceLoader.GetForCurrentView().GetString("ToastNotificationService_Title/Text");
            string description = string.Format(ResourceLoader.GetForCurrentView().GetString("ToastNotificationService_Description/Text"), Movie.TitleCn, Movie.Date);
            var content = new ToastContent()
            {
                Launch = "ToastContentActivationParams",

                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children =
                        {
                            new AdaptiveText()
                            {
                                Text = title
                            },

                            new AdaptiveText()
                            {
                                 Text = description
                            }       
                        },

                        HeroImage = new ToastGenericHeroImage()
                        {
                            Source = Movie.Image
                        }
                    }
                },

                Actions = new ToastActionsCustom()
                {
                    Buttons =
                    {
                        new ToastButtonDismiss("Oh Yeah")
                    }
                }
            };

            // Add the content to the toast
            var toast = new ToastNotification(content.GetXml())
            {
                Tag = "MoviePreviewToastAddToCollection"
            };

            // And show the toast
            ShowToastNotification(toast);
        }
    }
}
