using System;
using System.Threading.Tasks;

using Microsoft.Toolkit.Uwp.Notifications;
using MoviePreview.Models;
using Windows.UI.Notifications;
using Windows.UI.StartScreen;

namespace MoviePreview.Services
{
    internal partial class LiveTileService
    {
        // More about Live Tiles Notifications at https://docs.microsoft.com/windows/uwp/controls-and-patterns/tiles-and-notifications-sending-a-local-tile-notification
        // TODO 完善磁贴
        public void AddTileToQueue(string title, string titleEn, string tips, string tipsData, string detail, MovieItem item)
        {

            // Construct the tile content
            var content = new TileContent()
            {
                Visual = new TileVisual()
                {
                    Arguments = "Jennifer Parker",
                    TileMedium = new TileBinding()
                    {
                        Content = new TileBindingContentAdaptive()
                        {
                            PeekImage = new TilePeekImage()
                            {
                                Source = item.Image

                            },
                            Children =
                            {
                                new AdaptiveText()
                                {
                                    Text = title,
                                    HintStyle = AdaptiveTextStyle.Caption,
                                    HintAlign = AdaptiveTextAlign.Left
                                },
                                new AdaptiveText()
                                {
                                    Text = item.TitleCn,
                                    HintStyle = AdaptiveTextStyle.CaptionSubtle,
                                    HintAlign = AdaptiveTextAlign.Center
                                },
                                new AdaptiveText()
                                {
                                    Text = titleEn,
                                    HintStyle = AdaptiveTextStyle.CaptionSubtle
                                },
                                new AdaptiveText()
                                {
                                    Text = tips,
                                    HintAlign = AdaptiveTextAlign.Left
                                },
                                new AdaptiveText()
                                {
                                    Text = tipsData,
                                    HintAlign = AdaptiveTextAlign.Left
                                }
                            }
                        }
                    },

                    TileWide = new TileBinding()
                    {
                        Content = new TileBindingContentAdaptive()
                        {
                            TextStacking = TileTextStacking.Center,
                            Children =
                            {
                                new AdaptiveGroup()
                                {
                                    Children =
                                    {
                                        new AdaptiveSubgroup()
                                        {
                                            HintWeight = 1,
                                            Children =
                                            {
                                                new AdaptiveImage()
                                                {
                                                    Source = item.Image,
                                                    HintCrop = AdaptiveImageCrop.Circle
                                                }
                                            }
                                        },
                                        new AdaptiveSubgroup()
                                        {
                                            HintWeight = 1,
                                            Children =
                                            {
                                                new AdaptiveText()
                                                {
                                                    Text = title,
                                                    HintStyle = AdaptiveTextStyle.Caption,
                                                    HintAlign = AdaptiveTextAlign.Left
                                                },
                                                new AdaptiveText()
                                                {
                                                    Text = item.TitleCn,
                                                    HintStyle = AdaptiveTextStyle.CaptionSubtle,
                                                    HintAlign = AdaptiveTextAlign.Center
                                                },
                                                new AdaptiveText()
                                                {
                                                    Text = titleEn,
                                                    HintStyle = AdaptiveTextStyle.CaptionSubtle
                                                },
                                                new AdaptiveText()
                                                {
                                                    Text = tips,
                                                    HintAlign = AdaptiveTextAlign.Left
                                                },
                                                new AdaptiveText()
                                                {
                                                    Text = tipsData,
                                                    HintAlign = AdaptiveTextAlign.Left
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    },

                    TileLarge = new TileBinding()
                    {
                        Content = new TileBindingContentAdaptive()
                        {
                            Children =
                            {
                                new AdaptiveGroup()
                                {
                                    Children =
                                    {
                                        new AdaptiveSubgroup()
                                        {
                                            HintWeight = 1,
                                            Children =
                                            {
                                                new AdaptiveImage()
                                                {
                                                    Source = item.Image
                                                }
                                            }
                                        },
                                        new AdaptiveSubgroup()
                                        {
                                            HintWeight = 1,
                                            Children =
                                            {
                                                new AdaptiveText()
                                                {
                                                    Text = title,
                                                    HintStyle = AdaptiveTextStyle.Caption,
                                                    HintAlign = AdaptiveTextAlign.Left
                                                },
                                                new AdaptiveText()
                                                {
                                                    Text = item.TitleCn,
                                                    HintStyle = AdaptiveTextStyle.CaptionSubtle,
                                                    HintAlign = AdaptiveTextAlign.Center
                                                },
                                                new AdaptiveText()
                                                {
                                                    Text = titleEn,
                                                    HintStyle = AdaptiveTextStyle.CaptionSubtle
                                                },
                                                new AdaptiveText()
                                                {
                                                    Text = tips,
                                                    HintAlign = AdaptiveTextAlign.Left
                                                },
                                                new AdaptiveText()
                                                {
                                                    Text = tipsData,
                                                    HintAlign = AdaptiveTextAlign.Left
                                                }
                                            }
                                        }
                                    }
                                },
                                new AdaptiveText()
                                {
                                    Text = detail,
                                    HintWrap = true
                                }
                            }
                        }
                    }
                }
            };

            // Then create the tile notification
            var notification = new TileNotification(content.GetXml());
            UpdateTile(notification);
        }

        public async Task SamplePinSecondaryAsync(string pageName)
        {
            // TODO WTS: Call this method to Pin a Secondary Tile from a page.
            // You also must implement the navigation to this specific page in the OnLaunched event handler on App.xaml.cs
            var tile = new SecondaryTile(DateTime.Now.Ticks.ToString());
            tile.Arguments = pageName;
            tile.DisplayName = pageName;
            tile.VisualElements.Square44x44Logo = new Uri("ms-appx:///Assets/Square44x44Logo.scale-200.png");
            tile.VisualElements.Square150x150Logo = new Uri("ms-appx:///Assets/Square150x150Logo.scale-200.png");
            tile.VisualElements.Wide310x150Logo = new Uri("ms-appx:///Assets/Wide310x150Logo.scale-200.png");
            tile.VisualElements.ShowNameOnSquare150x150Logo = true;
            tile.VisualElements.ShowNameOnWide310x150Logo = true;
            await PinSecondaryTileAsync(tile);
        }
    }
}
