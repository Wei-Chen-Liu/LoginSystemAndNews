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
        MembersDBContext memberDB = new MembersDBContext();
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }


        public ActionResult Index()
        {
            memberDB.Members.FirstOrDefault();

            return View();
        }
    }
}
