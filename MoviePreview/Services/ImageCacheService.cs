using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace MoviePreview.Services
{
    public static class ImageCacheService
    {
        private static Dictionary<string, BitmapImage> imageCache;
        /// <summary>
        /// 从图片缓存器中获取图片
        /// </summary>
        /// <param name="uri">图片地址</param>
        /// <param name="decode">解析大小</param>
        /// <param name="defPic">默认图片</param>
        /// <returns></returns>
        public static BitmapImage GetImage(string uri, int decode = -1, string defPic = "background.jpg")
        {
            if (imageCache == null)
            {
                // 初始化图片缓存器
                imageCache = new Dictionary<string, BitmapImage>();
            }
            if (uri == null || uri == "") return new BitmapImage(new Uri($"ms-appx:///Assets/{defPic}"));

            if (!imageCache.ContainsKey(uri))
            {
                if (decode == -1)
                {
                    imageCache[uri] = new BitmapImage(new Uri(uri));
                }
                else
                {
                    imageCache[uri] = new BitmapImage()
                    {
                        UriSource = new Uri(uri),
                        DecodePixelHeight = decode,
                    };
                }
            }
            return imageCache[uri];
        }
    }
}
