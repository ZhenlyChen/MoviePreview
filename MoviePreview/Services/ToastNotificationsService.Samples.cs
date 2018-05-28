using Microsoft.Toolkit.Uwp.Notifications;
using MoviePreview.Models;
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
            string MovieName = Movie.TitleCn;
            string MovieDate = Movie.Date;
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
                                Text = "收藏的电影即将上映"
                            },

                            new AdaptiveText()
                            {
                                 Text = @"电影《" + MovieName + "》将于" + MovieDate + "上映"
                            }
                        }
                    }
                },

                Actions = new ToastActionsCustom()
                {
                    Buttons =
                    {
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
    }
}
