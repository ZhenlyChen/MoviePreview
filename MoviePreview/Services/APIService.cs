using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoviePreview.Helpers;
using MoviePreview.Models;
using Windows.Data.Json;

namespace MoviePreview.Services
{
    public static class TimeAPIService
    {
        // 地区码
        private static readonly string LocationGuangZhou = "365";
        // 正在热映
        private static readonly string APIHotPlayMovies = "https://api-m.mtime.cn/PageSubArea/HotPlayMovies.api?locationId={0}";
        // 本地电影
        private static readonly string APILocationMovies = "https://api-m.mtime.cn/Showtime/LocationMovies.api?locationId={0}";
        // 即将上映
        private static readonly string APIMovieComingNew = "https://api-m.mtime.cn/Movie/MovieComingNew.api?locationId={0}";
        // 影片详情
        private static readonly string APIMovieDetail = "https://ticket-api-m.mtime.cn/movie/detail.api?locationId={0}&movieId={1}";
        // 演员表
        private static readonly string APIMovieCredits = "https://api-m.mtime.cn/Movie/MovieCreditsWithTypes.api?movieId={0}";
        // 影片评论
        private static readonly string APIMovieComment = "https://ticket-api-m.mtime.cn/movie/hotComment.api?movieId={0}";
        // 预告片
        private static readonly string APIMovieVideo = "https://api-m.mtime.cn/Movie/Video.api?pageIndex=1&movieId={0}";
        // 剧照
        private static readonly string APIMovieImage = "https://api-m.mtime.cn/Movie/ImageAll.api?movieId={0}";
        // 电影搜索
        private static readonly string APIMovieSearch = "https://api-m.mtime.cn/Showtime/SearchVoice.api?Keyword={0}";

        /// <summary>
        /// 解析正在上映的电影
        /// </summary>
        /// <param name="value">Json值</param>
        /// <returns>MovieItemNow</returns>
        private static MovieItemNow ParseLocationMovie(IJsonValue value)
        {
            JsonObject m = value.GetObject();
            string dataStr = $"{m["rYear"].GetNumber().ToString()}-{m["rMonth"].GetNumber().ToString()}-{m["rDay"].GetNumber().ToString()}";
            return new MovieItemNow
            {
                Actor1 = m["actorName1"].GetString(),
                Actor2 = m["actorName2"].GetString(),
                CommonSpecial = m["commonSpecial"].GetString(),
                Directors = m["directorName"].GetString(),
                Image = m["img"].GetString().Replace("_1280X720X2", "_225X312"),
                MovieType = m["type"].GetString(),
                Rating = m["ratingFinal"].GetNumber(),
                // 转换日期格式
                Date = dataStr,

                TitleCn = m["titleCn"].GetString(),
                TitleEn = m["titleEn"].GetString(),
                WantedCount = (int)m["wantedCount"].GetNumber(),
                ID = m["movieId"].GetNumber().ToString(),
            };
        }

        /// <summary>
        /// 解析即将上映电影
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static MovieItemComing ParseComingMovie(IJsonValue value)
        {
            JsonObject m = value.GetObject();
            string dataStr = $"{m["rYear"].GetNumber().ToString()}-{m["rMonth"].GetNumber().ToString()}-{m["rDay"].GetNumber().ToString()}";
            return new MovieItemComing
            {
                Actor1 = m["actor1"].GetString(),
                Actor2 = m["actor2"].GetString(),
                Directors = m["director"].GetString(),
                ID = m["id"].GetNumber().ToString(),
                Image = m["image"].GetString().Replace("_1280X720X2", "_225X312"),
                LocationName = m["locationName"].GetString(),
                Date = dataStr,
                TitleCn = m["title"].GetString(),
                MovieType = m["type"].GetString(),
                WantedCount = (int)m["wantedCount"].GetNumber(),
            };
        }

        /// <summary>
        /// 解析电影详细Json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static MovieItemDetail ParseMovieDetail(JsonObject obj)
        {
            JsonObject m = obj["basic"].GetObject();
            string movieType = "";
            foreach (var t in m["type"].GetArray())
            {
                movieType += $" {t.GetString()}";
            }
            var b = obj["boxOffice"].GetObject();
            string strBox = $"{b["totalBoxDes"].GetString()} {b["totalBoxUnit"].GetString()}";
            return new MovieItemDetail
            {
                CommonSpecial = m["commentSpecial"].GetString(),
                Directors = (m["director"].GetObject())["name"].GetString(),
                ID = m["movieId"].GetNumber().ToString(),
                Image = m["img"].GetString(),
                MovieTime = m["mins"].GetString(),
                TitleCn = m["name"].GetString(),
                TitleEn = m["nameEn"].GetString(),
                LocationName = m["releaseArea"].GetString(),
                Date = DateTime.ParseExact(m["releaseDate"].GetString(), "yyyyMMdd",
                                  CultureInfo.InvariantCulture).ToString("yyyy-M-dd"),
                MovieType = movieType,
                Rating = m["overallRating"].GetNumber(),
                Story = m["story"].GetString(),
                Url = m["url"].GetString().Replace("https", "http"),
                TotalBox = strBox,
            };
        }

        /// <summary>
        /// 解析演员表
        /// </summary>
        /// <param name="m"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static MovieItemDetail ParseMoviePeople(MovieItemDetail m, JsonObject obj)
        {
            var list = obj["types"].GetArray();

            m.ActorList = new List<ActorItem>();
            m.DirectorList = new List<PeopleItem>();
            foreach (var t in list)
            {
                if ((t.GetObject())["typeNameEn"].GetString() == "Actor")
                {
                    foreach (var people in (t.GetObject())["persons"].GetArray())
                    {
                        var p = people.GetObject();
                        if (p["name"].GetString() == "" || p["personate"].GetString() == "") continue;
                        m.ActorList.Add(new ActorItem()
                        {
                            ID = p["id"].GetNumber().ToString(),
                            NameCn = p["name"].GetString(),
                            NameEn = p["nameEn"].GetString(),
                            Image = p["image"].GetString().Replace("_1280X720X2", "_100X100"),
                            RoleName = p["personate"].GetString(),
                            RoleImage = p.ContainsKey("roleCover") ? p["roleCover"].GetString() : "",
                            TypeName = (t.GetObject())["typeName"].GetString(),
                        });

                    }
                }
                else if ((t.GetObject())["typeNameEn"].GetString() == "Director")
                {
                    foreach (var people in (t.GetObject())["persons"].GetArray())
                    {
                        var p = people.GetObject();
                        m.DirectorList.Add(new PeopleItem()
                        {
                            ID = p["id"].GetNumber().ToString(),
                            NameCn = p["name"].GetString(),
                            NameEn = p["nameEn"].GetString(),
                            Image = p["image"].GetString().Replace("_1280X720X2", "_100X100"),
                            TypeName = (t.GetObject())["typeName"].GetString(),
                        });

                    }
                }
            }
            return m;
        }


        /// <summary>
        /// 解析评论
        /// </summary>
        /// <param name="m"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static MovieItemDetail ParseMovieComment(MovieItemDetail m, JsonObject obj)
        {
            var data = obj["data"].GetObject();
            m.Comments = new List<CommentItem>();
            var plusList = (data["plus"].GetObject())["list"].GetArray();
            foreach (var plus in plusList)
            {
                var com = plus.GetObject();
                m.Comments.Add(new CommentItem()
                {
                    Content = com["content"].GetString(),
                    HeadImg = com["headImg"].GetString(),
                    NickName = com["nickname"].GetString(),
                    Rating = com["rating"].GetNumber(),
                    LocationName = com["locationName"].GetString(),

                });
            }
            var miniList = (data["mini"].GetObject())["list"].GetArray();
            foreach (var mini in miniList)
            {
                var com = mini.GetObject();
                m.Comments.Add(new CommentItem()
                {
                    Content = com["content"].GetString(),
                    HeadImg = com["headImg"].GetString(),
                    NickName = com["nickname"].GetString(),
                    Rating = com["rating"].GetNumber(),
                    LocationName = com["locationName"].GetString(),

                });
            }
            return m;
        }

        /// <summary>
        /// 解析预告片
        /// </summary>
        /// <param name="m"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static MovieItemDetail ParseMovieVideo(MovieItemDetail m, JsonObject obj)
        {
            var list = obj["videoList"].GetArray();
            m.Videos = new List<VideoItem>();
            foreach (var video in list)
            {
                var v = video.GetObject();
                m.Videos.Add(new VideoItem()
                {
                    Image = v["image"].GetString(),
                    Url = v["hightUrl"].GetString(),
                    Title = v["title"].GetString(),
                    Length = (int)v["length"].GetNumber(),
                    Type = (int)v["type"].GetNumber(),
                });
            }
            return m;
        }

        /// <summary>
        /// 解析剧照
        /// </summary>
        /// <param name="m"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static MovieItemDetail ParseMovieImage(MovieItemDetail m, JsonObject obj)
        {
            var list = obj["images"].GetArray();
            int eachCount = 0;
            int currentType = 0;
            m.Images = new List<PostItem>();
            foreach (var i in list)
            {
                var img = i.GetObject();
                // 每种剧照只获取3张
                int typeNew = (int)img["type"].GetNumber();
                if (currentType == typeNew)
                {
                    if (eachCount > 2) continue;
                    eachCount++;
                }
                else
                {
                    eachCount = 0;
                    currentType = typeNew;
                }
                m.Images.Add(new PostItem()
                {
                    ID = img["id"].GetNumber().ToString(),
                    Title = img["imageSubtypeDes"].GetString(),
                    Image = img["image"].GetString(),
                });
            }
            return m;
        }

        /// <summary>
        /// 获取当前地区的上映电影
        /// API: APILocationMovies
        /// </summary>
        /// <param name="location">地区，默认为广州,后期再加上定位和选择</param>
        /// <returns>电影列表</returns>
        public static async Task<List<MovieItemNow>> GetLocationMovies(string location = "365")
        {
            var movieList = new List<MovieItemNow>();
            JsonObject res = await Singleton<NetService>.Instance.GetJson(string.Format(APIHotPlayMovies, location));
            if (res == null)
            {
                return movieList;
            }
            JsonArray ms = res["movies"].GetArray();
            foreach (var m in ms)
            {
                var movie = ParseLocationMovie(m);
                if (movie.Rating > 0)
                {
                    movieList.Add(movie);
                }
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
            var movieList = new List<MovieItemComing>();
            JsonObject res = await Singleton<NetService>.Instance.GetJson(string.Format(APIMovieComingNew, location));
            if (res == null)
            {
                return movieList;
            }
            // 加入热门电影
            JsonArray attentions = res["attention"].GetArray();
            foreach (var m in attentions)
            {
                var movie = ParseComingMovie(m);
                movie.Hot = true;
                movieList.Add(movie);
            }

            JsonArray moviecomings = res["moviecomings"].GetArray();
            foreach (var m in moviecomings)
            {
                var movie = ParseComingMovie(m);

                // 过滤已存在电影
                bool flag = false;
                foreach (var had in movieList)
                {
                    if (had.ID == movie.ID) flag = true;
                }
                if (flag == true) continue;

                movie.Hot = false;
                movieList.Add(movie);
            }
            return movieList;
        }

        public static MovieItemDetail CurrentDetail;
        public static Dictionary<string, MovieItemDetail> GetedDetail;
        /// <summary>
        /// 获取电影详情(包括详情，影评，预告片，剧照，演员表)
        /// 需使用5个API
        /// </summary>
        /// <param name="id">电影ID</param>
        /// <returns></returns>
        public static async Task<MovieItemDetail> GetMovieDetail(string id)
        {
            // 详情
            try
            {
                JsonObject detail = await Singleton<NetService>.Instance.GetJson(string.Format(APIMovieDetail, LocationGuangZhou, id));
                var movieDetail = ParseMovieDetail(detail["data"].GetObject());

                // 演员表
                JsonObject people = await Singleton<NetService>.Instance.GetJson(string.Format(APIMovieCredits, id));
                movieDetail = ParseMoviePeople(movieDetail, people);

                // 影评
                JsonObject comments = await Singleton<NetService>.Instance.GetJson(string.Format(APIMovieComment, id));
                movieDetail = ParseMovieComment(movieDetail, comments);

                // 预告片
                JsonObject videos = await Singleton<NetService>.Instance.GetJson(string.Format(APIMovieVideo, id));
                movieDetail = ParseMovieVideo(movieDetail, videos);

                // 剧照
                JsonObject images = await Singleton<NetService>.Instance.GetJson(string.Format(APIMovieImage, id));
                movieDetail = ParseMovieImage(movieDetail, images);

                CurrentDetail = movieDetail;
                if (GetedDetail == null) GetedDetail = new Dictionary<string, MovieItemDetail>();
                GetedDetail[movieDetail.ID] = movieDetail;
                return movieDetail;
            }
            catch (Exception)
            {
                return new MovieItemDetail();
            }
        }

        public static async Task<Dictionary<string, string>> GetMoviesTitleList(string query)
        {
            JsonObject res = await Singleton<NetService>.Instance.GetJson(string.Format(APIMovieSearch, query));
            if (res == null)
            {
                return new Dictionary<string, string>();
            }
            var map = new Dictionary<string, string>();
            JsonArray movies = res["movies"].GetArray();
            foreach (var obj in movies)
            {
                JsonObject movie = obj.GetObject();
                string id = movie["id"].GetNumber().ToString();
                string title = movie["name"].GetString();
                map[title] = id;
            }
            return map;
        }
    }
}
