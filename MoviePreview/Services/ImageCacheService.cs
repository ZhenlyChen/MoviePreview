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
        public static BitmapImage GetImage(string uri, int decode = -1)
        {
            if (imageCache == null)
            {
                imageCache = new Dictionary<string, BitmapImage>();
                imageCache[""] = new BitmapImage(new Uri("ms-appx:///Assets/background.jpg"));
            }
            if (uri == null) return imageCache[""];
            if (!imageCache.ContainsKey(uri))
            {
                if (decode == -1)
                {
                    imageCache[uri] = new BitmapImage(new Uri(uri));
                } else
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
