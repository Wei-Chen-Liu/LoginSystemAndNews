using LoginSystemAndNews.Interfaces;
using LoginSystemAndNews.Models.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginSystemAndNews.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;
        private readonly ILoginTimeLogService _loginTimeLogService;

        public MemberController(IMemberService memberService, ILoginTimeLogService loginTimeLogService)
        {
            _memberService = memberService;
            _loginTimeLogService = loginTimeLogService;
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignIn()
        {
            if (_memberService != null)
            {
                



                return View();
            }

            return View();
        }

        public ActionResult Register()
        {
            if (_memberService != null)
            {




                return View();
            }

            return View();
        }

        public ActionResult ForgetPassword()
        {
            if (_memberService != null)
            {




                return View();
            }

            return View();
        }

        public ActionResult ResetPassword()
        {
            if (_memberService != null)
            {




                return View();
            }

            return View();
        }
    }
}
