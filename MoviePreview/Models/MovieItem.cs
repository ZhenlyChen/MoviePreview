using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace MoviePreview.Models
{

    public class MovieItem
    { // 即将上映的电影
        // ID
        public string ID {
            get; set;
        }
        // 中文名称
        public string TitleCn {
            get; set;
        }
        // 类型
        public string MovieType {
            get; set;
        }
        // 主演1
        public string Actor1 {
            get; set;
        }
        // 主演2
        public string Actor2 {
            get; set;
        }
        // 主要演员
        public string Actors {
            get; set;
        }
        // 导演
        public string Directors {
            get; set;
        }
        // 想看人数
        public int WantedCount {
            get; set;
        }
        // 年份
        public string Year {
            get; set;
        }
        // 封面
        public string Image {
            get; set;
        }
        // 上映日期
        public string Date {
            get; set;
        }
        // ---- 生成属性 ——--
        // 海报URL
        public BitmapImage ImageUri {
            get {
                return new BitmapImage(new Uri(Image));
            }
        }
    }

    public class MovieItemComing : MovieItem
    {

    }

    public class MovieItemNow : MovieItem
    { // 已经上映的电影
        // -----已上映电影属性 -----
        // 英文名
        public string TitleEn {
            get; set;
        }
        // 评分
        public double Rating {
            get; set;
        }
        // 一句话简介
        public string CommonSpecial {
            get; set;
        }
        // 片长
        public string Length {
            get; set;
        }
    }

    public class MovieItemDetail : MovieItemNow
    {
        // 演员
        public List<ActorItem> ActorList {
            get; set;
        }
        // 导演
        public List<PeopleItem> DirectorList {
            get; set;
        }
        // 其他人员
        public List<PeopleItem> OtherList {
            get; set;
        }
        // 剧情
        public string Story {
            get; set;
        }
        // 剧照
        public List<string> Images {
            get; set;
        }
        // 票房
        public string TotalBox {
            get; set;
        }
        // 影评
        public List<CommentItem> Comments {
            get; set;
        }
        // 长评
        public List<CommentPlusItem> CommentsPlus {
            get; set;
        }
        // 预告片
        public List<VideoItem> Videos {
            get; set;
        }
    }

    // 预告片
    public class VideoItem
    {
        // 标题
        public string Title {
            get; set;
        }
        // 图片
        public string Image {
            get; set;
        }
        // 视频
        public string Url {
            get; set;
        }
        // 片长
        public int Length {
            get; set;
        }
    }

    // 影评
    public class CommentItem
    {
        // 昵称
        public string NickName {
            get; set;
        }
        // 地区
        public string LocationName {
            get; set;
        }
        // 内容
        public string Content {
            get; set;
        }
        // 头像
        public string HeadImg {
            get; set;
        }
    }

    // 长评论
    public class CommentPlusItem : CommentItem
    {
        public string Title {
            get; set;
        }
    }


    public class PeopleItem
    {
        // ID
        public int ID {
            get; set;
        }
        // 照片
        public string Image {
            get; set;
        }
        // 中文名
        public string NameCn {
            get; set;
        }
        // 英文名
        public string NameEn {
            get; set;
        }
        // 类型：演员/导演/编剧/服装设计
        public string TypeName {
            get; set;
        }
    }

    public class ActorItem : PeopleItem
    { // 演员
        // 角色名
        public string RoleName {
            get; set;
        }
        // 角色照
        public string RoleImage {
            get; set;
        }
    }


}
