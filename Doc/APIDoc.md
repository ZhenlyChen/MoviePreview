# 豆瓣API

[TOC]



## 文档

https://developers.douban.com/wiki/?title=movie_v2

中文均为Unicode编码

部分接口为商务代码，商务要求：

- 评分需和资料任一项同时显示，并且标注“豆瓣评分”
- 如使用资料，须在影片资料下增加链接“去豆瓣电影查看详情”，链接指向为相应条目url
- 影评须单独注明“豆瓣电影”，不可和其他方评论数据混合显示
- 如果应用包含客户端服务，此客户端不得使用包含“豆瓣“和”豆瓣电影”字样显示在名称及相应logo内
- 如果应用使用豆瓣资料、评分、影评，则此应用不得采用其他平台方提供的同类型数据，且应用不得自建评分、影评服务

## 具体

返回均为JSON

### 当前上映(商务接口)

https://api.douban.com/v2/movie/in_theaters

返回结果实例：

```json
{
  "count": 20, // 获取到的结果包含电影总数
  "start": 0,
  "total": 26, // 并不知道有扫描用
  "subjects": [{ // 电影集合
    "rating": {
      "max": 10,
      "average": 8.4, // 当前评分
      "stars": "45", // 评星数（不知道是什么）
      "min": 0
    },
    "genres": ["\u52a8\u4f5c", "\u79d1\u5e7b", "\u5947\u5e7b"], // 类型 ： “动作", "科幻", "奇幻"
    "title": "\u590d\u4ec7\u8005\u8054\u76df3\uff1a\u65e0\u9650\u6218\u4e89", // 标题： 复仇者联盟3：无限战争
    "casts": [{ // 演员集合
      "alt": "https:\/\/movie.douban.com\/celebrity\/1016681\/", // 演员主页
      "avatars": { // 演员头像url
        "small": "https://img1.doubanio.com\/view\/celebrity\/s_ratio_celebrity\/public\/p56339.webp",
        "large": "https://img1.doubanio.com\/view\/celebrity\/s_ratio_celebrity\/public\/p56339.webp",
        "medium": "https://img1.doubanio.com\/view\/celebrity\/s_ratio_celebrity\/public\/p56339.webp"
      },
      "name": "\u5c0f\u7f57\u4f2f\u7279\u00b7\u5510\u5c3c", // 演员名字 小罗伯特·唐尼
      "id": "1016681"// 演员ID
    },{
        // ...
    }],
    "collect_count": 280959, // 看过的人数
    "original_title": "Avengers: Infinity War", // 原名
    "subtype": "movie", // 类型
    "directors": [{ //导演
      "alt": "https:\/\/movie.douban.com\/celebrity\/1321812\/",
      "avatars": {
        "small": "https://img3.doubanio.com\/view\/celebrity\/s_ratio_celebrity\/public\/p51466.webp",
        "large": "https://img3.doubanio.com\/view\/celebrity\/s_ratio_celebrity\/public\/p51466.webp",
        "medium": "https://img3.doubanio.com\/view\/celebrity\/s_ratio_celebrity\/public\/p51466.webp"
      },
      "name": "\u5b89\u4e1c\u5c3c\u00b7\u7f57\u7d20",
      "id": "1321812"
    }, {
      // ...
    }],
    "year": "2018", // 年份
    "images": { // 电影海报URL
      "small": "https://img3.doubanio.com\/view\/photo\/s_ratio_poster\/public\/p2517753454.webp",
      "large": "https://img3.doubanio.com\/view\/photo\/s_ratio_poster\/public\/p2517753454.webp",
      "medium": "https://img3.doubanio.com\/view\/photo\/s_ratio_poster\/public\/p2517753454.webp"
    },
    "alt": "https:\/\/movie.douban.com\/subject\/24773958\/", // 电影主页
    "id": "24773958" // 电影id
  }, {
      // ...
  }]
}
```



### 电影详情

https://api.douban.com/v2/movie/subject/:id

```json
{
  "rating": { // 评分
    "max": 10,
    "average": 8.4,
    "stars": "45",
    "min": 0
  },
  "reviews_count": 3005, // 影评数量
  "wish_count": 64007, // 想看人数
  "douban_site": "",
  "year": "2018",
  "images": { // 海报
    "small": "https://img3.doubanio.com\/view\/photo\/s_ratio_poster\/public\/p2517753454.webp",
    "large": "https://img3.doubanio.com\/view\/photo\/s_ratio_poster\/public\/p2517753454.webp",
    "medium": "https://img3.doubanio.com\/view\/photo\/s_ratio_poster\/public\/p2517753454.webp"
  },
  "alt": "https:\/\/movie.douban.com\/subject\/24773958\/",
  "id": "24773958",
  "mobile_url": "https:\/\/movie.douban.com\/subject\/24773958\/mobile",
  "title": "\u590d\u4ec7\u8005\u8054\u76df3\uff1a\u65e0\u9650\u6218\u4e89",
  "do_count": null,
  "share_url": "https:\/\/m.douban.com\/movie\/subject\/24773958",
  "seasons_count": null,
  "schedule_url": "https:\/\/movie.douban.com\/subject\/24773958\/cinema\/",
  "episodes_count": null,
  "countries": ["\u7f8e\u56fd"], // 国家
  "genres": ["\u52a8\u4f5c", "\u79d1\u5e7b", "\u5947\u5e7b"],
  "collect_count": 281005, // 看过人数
  "casts": [{ // 演员
    "alt": "https:\/\/movie.douban.com\/celebrity\/1016681\/",
    "avatars": {
      "small": "https://img1.doubanio.com\/view\/celebrity\/s_ratio_celebrity\/public\/p56339.webp",
      "large": "https://img1.doubanio.com\/view\/celebrity\/s_ratio_celebrity\/public\/p56339.webp",
      "medium": "https://img1.doubanio.com\/view\/celebrity\/s_ratio_celebrity\/public\/p56339.webp"
    },
    "name": "\u5c0f\u7f57\u4f2f\u7279\u00b7\u5510\u5c3c",
    "id": "1016681"
  }, {
    // ...
  }],
  "current_season": null,
  "original_title": "Avengers: Infinity War",
  "summary": "\u300a\u590d\u4ec7\u8005\u8054\u76df3\uff1a\u65e0\u9650\u6218\u4e89\u300b\u662f\u6f2b\u5a01\u7535\u5f71\u5b87\u5b9910\u5468\u5e74\u7684\u5386\u53f2\u6027\u96c6\u7ed3\uff0c\u5c06\u4e3a\u5f71\u8ff7\u4eec\u5e26\u6765\u53f2\u8bd7\u7248\u7684\u7ec8\u6781\u5bf9\u51b3\u3002\u9762\u5bf9\u706d\u9738\u7a81\u7136\u53d1\u8d77\u7684\u95ea\u7535\u88ad\u51fb\uff0c\u590d\u4ec7\u8005\u8054\u76df\u53ca\u5176\u6240\u6709\u8d85\u7ea7\u82f1\u96c4\u76df\u53cb\u5fc5\u987b\u5168\u529b\u4ee5\u8d74\uff0c\u624d\u80fd\u963b\u6b62\u4ed6\u5bf9\u5168\u5b87\u5b99\u9020\u6210\u6bc1\u706d\u6027\u7684\u6253\u51fb\u3002", // 简介
  "subtype": "movie",
  "directors": [{
    "alt": "https:\/\/movie.douban.com\/celebrity\/1321812\/",
    "avatars": {
      "small": "https://img3.doubanio.com\/view\/celebrity\/s_ratio_celebrity\/public\/p51466.webp",
      "large": "https://img3.doubanio.com\/view\/celebrity\/s_ratio_celebrity\/public\/p51466.webp",
      "medium": "https://img3.doubanio.com\/view\/celebrity\/s_ratio_celebrity\/public\/p51466.webp"
    },
    "name": "\u5b89\u4e1c\u5c3c\u00b7\u7f57\u7d20",
    "id": "1321812"
  }, {
   // ...
  }],
  "comments_count": 102975, // 短评数量
  "ratings_count": 266891, // 评分数量
  "aka": ["\u590d\u80543", "\u590d\u4ec7\u8005\u8054\u76df\uff1a\u65e0\u9650\u4e4b\u6218(\u53f0)", "\u590d\u4ec7\u8005\u8054\u76df3\uff1a\u65e0\u5c3d\u4e4b\u6218", "Avengers: Infinity War - Part I", "The Avengers 3: Part 1"] // 更多中文名
}

```



### 演员详情

https://api.douban.com/v2/movie/celebrity/:id

```json
{
  "mobile_url": "https:\/\/movie.douban.com\/celebrity\/1390831\/mobile",
  "aka_en": [],
  "name": "\u7f57\u6d0b", // 名称
  "works": [ // 作品
    {
      "roles": [
        "\u5bfc\u6f14" // 角色：导演
      ],
      "subject": { // 作品内容
       //...
      }
    }, {
        ...
    }
  ],
  "gender": "",
  "avatars": {
    "small": "https://img3.doubanio.com\/view\/celebrity\/s_ratio_celebrity\/public\/p1522392000.73.webp",
    "large": "https://img3.doubanio.com\/view\/celebrity\/s_ratio_celebrity\/public\/p1522392000.73.webp",
    "medium": "https://img3.doubanio.com\/view\/celebrity\/s_ratio_celebrity\/public\/p1522392000.73.webp"
  },
  "id": "1390831",
  "aka": [],
  "name_en": "Yang Luo", //英文名
  "born_place": "",
  "alt": "https:\/\/movie.douban.com\/celebrity\/1390831\/"
}
```






### 电影搜索

https://api.douban.com/v2/movie/search?q={text}

参数：
q	：query string
tag	：tag query string
start	：start
count ：count

```json
{ 
    "count": 20, "start": 0, "total": 50, 
 	"subjects": [],
 	"title": "\u641c\u7d22 \"\u5934\u53f7\u73a9\u5bb6\" \u7684\u7ed3\u679c"
}
```



### Top250

https://api.douban.com/v2/movie/top250
参数：
start, count

```json
{
    "count": 20, "start": 0, "total": 250,
    "subjects": [],
     "title": "\u8c46\u74e3\u7535\u5f71Top250"
}
```



### 北美票房榜(1-11)

https://api.douban.com/v2/movie/us_box

```json
{
  "date": "5\u670818\u65e5 - 5\u670820\u65e5", // "5月18日 - 5月20日
  "subjects": [],
  "title": "\u8c46\u74e3\u7535\u5f71\u5317\u7f8e\u7968\u623f\u699c",
 }
```



### 即将上映(商务接口)

https://api.douban.com/v2/movie/coming_soon

参数：
start, count

```json
{ 
    "count": 20, "start": 0, "total": 118,
    "subjects": [],
  	"title": "\u5373\u5c06\u4e0a\u6620\u7684\u7535\u5f71"
}
```



