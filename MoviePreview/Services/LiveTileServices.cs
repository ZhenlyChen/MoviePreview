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
        /// <summary>
        /// 添加磁贴
        /// </summary>
        /// <param name="title"></param>
        /// <param name="titleEn"></param>
        /// <param name="tips"></param>
        /// <param name="tipsData"></param>
        /// <param name="detail"></param>
        /// <param name="item"></param>
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
    }
}
