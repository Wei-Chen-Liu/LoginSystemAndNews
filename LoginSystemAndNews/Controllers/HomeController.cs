using LoginSystemAndNews.Models.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoginSystemAndNews.Controllers;
using LoginSystemAndNews.Interfaces;
using LoginSystemAndNews.Models.News;

namespace LoginSystemAndNews.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMemberService _memberService;
        private readonly ILoginTimeLogService _loginTimeLogService;
        private readonly INewsService _newsService;

        public HomeController(IMemberService memberService, ILoginTimeLogService loginTimeLogService, INewsService newsService)
        {
            _memberService = memberService;
            _loginTimeLogService = loginTimeLogService;
            _newsService = newsService;
        }

        public ActionResult Index()
        {
            ViewBag.allNewsData = _newsService.GetAllNews();

            return View();
        }

        public ActionResult EachNews(int newsId)
        {
            ViewBag.allNewsData = _newsService.GetNewsById(newsId);

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddNews(FormCollection formData)
        {
            News news = new News();
            news.Title = formData["news-title-input"];
            news.Category = formData["category"];
            news.Content = formData["news-content-input"];

            _newsService.AddNews(news);


            return RedirectToAction("Index");
        }

    }
}
