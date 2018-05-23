using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoviePreview.Models;
using Windows.Data.Json;

namespace MoviePreview.Services
{
    public static class TimeAPIService
    {
        // 地区码
        private static readonly string LocationGuangZhou = "365";
        // 正在热映
        private static readonly string APILocationMovies = "https://api-m.mtime.cn/Showtime/LocationMovies.api?locationId={0}";
        // 即将上映
        private static readonly string APIMovieComingNew = "https://api-m.mtime.cn/Movie/MovieComingNew.api?locationId={0}";
        // 影片详情
        private static readonly string APIMovieDetail = "https://ticket-api-m.mtime.cn/movie/detail.api?locationId={0}&movieId={1}";
        // 演员表
        private static readonly string APIMovieCredits = "https://api-m.mtime.cn/Movie/MovieCreditsWithTypes.api?movieId={1}";
        // 影片评论
        private static readonly string APIMovieComment = "https://ticket-api-m.mtime.cn/movie/hotComment.api?movieId={1}";
        // 预告片
        private static readonly string APIMovieVideo = "https://api-m.mtime.cn/Movie/Video.api?pageIndex=1&movieId={1}";
        // 剧照
        private static readonly string APIMovieImage = "https://api-m.mtime.cn/Movie/ImageAll.api?movieId={1}";

        /// <summary>
        /// 解析正在上映的电影
        /// </summary>
        /// <param name="value">Json值</param>
        /// <returns>MovieItemNow</returns>
        private static MovieItemNow ParseLocationMovie(IJsonValue value)
        {
            JsonObject m = value.GetObject();
            return new MovieItemNow
            {
                Actor1 = m["aN1"].GetString(),
                Actor2 = m["aN2"].GetString(),
                Actors = m["actors"].GetString(),
                CommonSpecial = m["commonSpecial"].GetString(),
                Length = m["d"].GetString(),
                Directors = m["dN"].GetString(),
                Image = m["img"].GetString(),
                MovieType = m["movieType"].GetString(),
                Rating = m["r"].GetNumber(),
                // 转换日期格式
                Date = DateTime.ParseExact(m["rd"].GetString(), "yyyyMMdd",
                                  CultureInfo.InvariantCulture).ToString("yyyy/M/dd"),
                TitleCn = m["tCn"].GetString(),
                TitleEn = m["tEn"].GetString(),
                WantedCount = (int)m["wantedCount"].GetNumber(),
                Year = m["year"].GetString()
            };
        }

        /// <summary>
        /// 获取当前地区的上映电影
        /// API: APILocationMovies
        /// </summary>
        /// <param name="location">地区，默认为广州,后期再加上定位和选择</param>
        /// <returns>电影列表</returns>
        public static async Task<List<MovieItemNow>> GetLocationMovies(string location = "365")
        {
            JsonObject res = await NetService.GetJson(string.Format(APILocationMovies, location));
            JsonArray ms = res["ms"].GetArray();
            var movieList = new List<MovieItemNow>();
            foreach (var m in ms)
            {
                movieList.Add(ParseLocationMovie(m));
            }
            return movieList;
        }

        /// <summary>
        /// 获取当前地区即将上映电影
        /// API: APIMovieComingNew
        /// </summary>
        /// <param name="location">地区，默认为广州</param>
        /// <returns>电影列表</returns>
        public static async Task<List<MovieItemComing>> GetComingMovies(string location = "365")
        {
            // TODO API
            return null;
        }

        /// <summary>
        /// 获取电影详情(包括详情，影评，预告片，剧照，演员表)
        /// 需使用5个API
        /// </summary>
        /// <param name="id">电影ID</param>
        /// <returns></returns>
        public static async Task<List<MovieItemDetail>> GetMovieDetail(string id)
        {
            // TODO API
            return null;
        }

    }
}
