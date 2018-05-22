using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace MoviePreview.Models {
    public class MovieItem { // 电影
        // ID
        public string ID {
            get; set;
        }
        // 名称
        public string Title {
            get;set;
        }
        // 原名
        public string OriginalTitle {
            get;set;
        }
        // 评分
        public double Rating {
            get;set;
        }
        // 类型
        public string[] Genres {
            get;set;
        }
        // 演员
        public PeopleItem[] Casts {
            get;set;
        }
        // 导演
        public PeopleItem[] Directors {
            get;set;
        }
        // 看过人数
        public int CollectCount {
            get;set;
        }
        // 年份
        public string Year {
            get;set;
        }
        // 封面
        public string Image {
            get;set;
        }
        // 豆瓣URL
        public string Alt {
            get;set;
        }
        // ----- 以下为详细信息 -----
        // 想看人数
        public int WishCount {
            get;set;
        }
        // 国家
        public string[] Countries {
            get;set;
        }
        // 简介
        public string Summary {
            get;set;
        }
        // 别名
        public string[] Aka {
            get;set;
        }
        // ---- 生成属性 ——--
        // 海报URL
        public BitmapImage ImageUri {
            get {
                return new BitmapImage(new Uri(Image));
            }
        }
    

    }

    public class PeopleItem {
        // ID
        public string ID {
            get; set;
        }
        // 名称
        public string Name {
            get;set;
        }
        // 头像
        public string Avatar {
            get;set;
        }
        // 豆瓣URL
        public string Alt {
            get; set;
        }
    }
    
}
