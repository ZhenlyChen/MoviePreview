using MoviePreview.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace MoviePreview.Services {
    public static class APIService {
        // 正在上映 | query参数：city, start, count
        private static readonly string APIMoviesInTheaters = "https://api.douban.com/v2/movie/in_theaters?city={0}&start={1}&count={2}";
        // 即将上映 | query参数：start, count
        private static readonly string APIComingMovies = "https://api.douban.com/v2/movie/coming_soon?start={0}&count={1}";
        // 电影详情 | path参数：id
        private static readonly string APIMovieDetail = "https://api.douban.com/v2/movie/subject/{0}";
        // 人物详情 | path参数：id
        private static readonly string APIPeopleDetail = "https://api.douban.com/v2/movie/celebrity/{0}";
        // 通过名称搜索 | query参数：q, start, count
        private static readonly string APISearchMovieByName = "https://api.douban.com/v2/movie/search?q={text}&start={1}&count={2}";
        // 通过类型搜索 | query参数：tag, start, count
        private static readonly string APISearchMovieByTag = "https://api.douban.com/v2/movie/search?tag={text}&start={1}&count={2}";
        // Top250 | query参数：start, count
        private static readonly string APITopMovies250 = "https://api.douban.com/v2/movie/top250?start={0}&count={1}";
        // 北美票房榜
        private static readonly string APITopMoviesInUs = "https://api.douban.com/v2/movie/us_box";

        /// <summary>
        /// 将JSON对象转换成PeopleItem
        /// </summary>
        /// <param name="subject">People对应的Json对象cast或director</param>
        /// <returns>PeopleItem</returns>
        private static PeopleItem ParseJsonToPeople(JsonObject people) {
            PeopleItem peopleItem = new PeopleItem {
                Name = people["name"].GetString(),
                Avatar = (people["avatars"].GetObject())["medium"].GetString(), // 获取中等大小的图片(small,large,medium)
                Alt = people["alt"].GetString(),
                ID = people["id"].GetString()
            };
            return peopleItem;
        }

        /// <summary>
        /// 将JSON对象转换成MovieItem
        /// </summary>
        /// <param name="movie">Moive对应的Json对象subjects</param>
        /// <returns>MovieItem</returns>
        private static MovieItem ParseJsonToMovie(JsonObject movie) {
            // 解析基本信息
            MovieItem movieItem = new MovieItem {
                Rating = (movie["rating"].GetObject())["average"].GetNumber(),
                Title = movie["title"].GetString(),
                CollectCount = (int)movie["collect_count"].GetNumber(),
                OriginalTitle = movie["original_title"].GetString(),
                Year = movie["year"].GetString(),
                Image = (movie["images"].GetObject())["medium"].GetString(), // 获取中等大小的图片(small,large,medium)
                Alt = movie["alt"].GetString(),
                ID = movie["id"].GetString()
            };

            var genresList = new List<string>();
            JsonArray genres = movie["genres"].GetArray();
            foreach (var genre in genres) {
                genresList.Add(genre.GetString());
            }
            movieItem.Genres = genresList.ToArray();

            var castsList = new List<PeopleItem>();
            JsonArray casts = movie["casts"].GetArray();
            foreach (var cast in casts) {
                castsList.Add(ParseJsonToPeople(cast.GetObject()));
            }
            movieItem.Casts = castsList.ToArray();

            var directorsList = new List<PeopleItem>();
            JsonArray directors = movie["directors"].GetArray();
            foreach (var director in directors) {
                directorsList.Add(ParseJsonToPeople(director.GetObject()));
            }
            movieItem.Directors = directorsList.ToArray();


            // 解析详细信息
            if (movie.ContainsKey("summary")) {
                movieItem.Summary = movie["summary"].GetString();
                movieItem.WishCount = (int)movie["wish_count"].GetNumber();

                var countriesList = new List<string>();
                JsonArray countries = movie["countries"].GetArray();
                foreach (var country in countries) {
                    countriesList.Add(country.GetString());
                }
                movieItem.Countries = countriesList.ToArray();

                var akaList = new List<string>();
                JsonArray aka = movie["aka"].GetArray();
                foreach (var name in aka) {
                    akaList.Add(name.GetString());
                }
                movieItem.Aka = akaList.ToArray();
            }
            return movieItem;
        }

        /// <summary>
        /// 获取正在上映电影
        /// </summary>
        /// <param name="city">城市， 默认为"广州", 后期再加定位系统</param>
        /// <param name="page">第几页</param>
        /// <param name="eachPage">每页有多少个，默认10</param>
        /// <returns>电影列表, 列表项目总数</returns>
        public static async Task<Tuple<List<MovieItem>, int>> GetInTheaters(string city, int page, int eachPage) {
            JsonObject res = await NetService.GetJson(string.Format(APIMoviesInTheaters, city, (page - 1) * eachPage, eachPage));

            if (!res.ContainsKey("count")) {
                return null;
            }

            var moviesList = new List<MovieItem>();
            JsonArray movies = res["subjects"].GetArray();
            foreach (var movie in movies) {
                moviesList.Add(ParseJsonToMovie(movie.GetObject()));
            }
            return Tuple.Create(moviesList, (int)res["total"].GetNumber());
        }

        /// <summary>
        /// 获取即将上映电影
        /// </summary>
        /// <param name="page">第几页</param>
        /// <param name="eachPage">每页有多少个，默认10</param>
        /// <returns>电影列表, 列表项目总数</returns>
        public static async Task<Tuple<List<MovieItem>, int>> GetComing(int page, int eachPage) {
            // todo
            return null;
        }

        /// <summary>
        /// 获取北美票房榜
        /// 总数为固定值（11），一次性返回全部
        /// </summary>
        /// <returns>电影列表</returns>
        public static List<MovieItem> GetTopInUs() {
            // todo
            return null;
        }


        /// <summary>
        /// 获取Top250榜单
        /// 总数为固定值250
        /// </summary>
        /// <param name="page">第几页</param>
        /// <param name="eachPage">每页有多少个，默认10</param>
        /// <returns>电影列表</returns>
        public static List<MovieItem> GetTop250(int page, int eachPage) {
            // todo
            return null;
        }

        /// <summary>
        /// 获取电影详情
        /// </summary>
        /// <param name="id">电影ID</param>
        /// <returns>电影详情</returns>
        public static MovieItem GetMovieDetail(string id) {
            // todo
            return null;
        }

        /// <summary>
        /// 获取人物详情（暂时不需要）
        /// </summary>
        /// <param name="id">人物ID</param>
        /// <returns>人物详情</returns>
        public static PeopleItem GetPeopleDetail(string id) {
            // Nothing
            return null;
        }

        /// <summary>
        /// 通过名称查询电影
        /// </summary>
        /// <param name="name">查询关键字</param>
        /// <param name="page">第几页</param>
        /// <param name="eachPage">每页多少个项， 默认10</param>
        /// <returns>电影列表, 列表项目总数</returns>
        public static async Task<Tuple<List<MovieItem>, int>> SearchByName(string name, int page, int eachPage) {
            // todo
            return null;
        }

        /// <summary>
        /// 通过类型查询电影
        /// </summary>
        /// <param name="tag">查询关键字</param>
        /// <param name="page">第几页</param>
        /// <param name="eachPage">每页多少个项， 默认10</param>
        /// <returns>电影列表, 列表项目总数</returns>
        public static async Task<Tuple<List<MovieItem>, int>> SearchByTag(string tag, int page, int eachPage) {
            // todo
            return null;
        }

    }
}
