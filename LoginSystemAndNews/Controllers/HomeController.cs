using LoginSystemAndNews.Models.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoginSystemAndNews.Controllers;
using LoginSystemAndNews.Interfaces;

namespace LoginSystemAndNews.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMemberService _memberService;
        private readonly ILoginTimeLogService _loginTimeLogService;

        public HomeController(IMemberService memberService, ILoginTimeLogService loginTimeLogService)
        {
            _memberService = memberService;
            _loginTimeLogService = loginTimeLogService;
        }

        public ActionResult Index()
        {

            return View();
        }
    }
}
