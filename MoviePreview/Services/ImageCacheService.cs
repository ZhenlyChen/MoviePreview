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
        public static BitmapImage GetImage(string uri)
        {
            if (imageCache == null)
            {
                imageCache = new Dictionary<string, BitmapImage>();
                imageCache[""] = new BitmapImage(new Uri("ms-appx:///Assets/background.jpg"));
            }
            if (uri == null) return imageCache[""];
            if (!imageCache.ContainsKey(uri))
            {
                imageCache[uri] = new BitmapImage(new Uri(uri));
            }
            return imageCache[uri];
        }
    }
}
