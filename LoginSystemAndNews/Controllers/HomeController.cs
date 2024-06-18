using LoginSystemAndNews.Models.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginSystemAndNews.Controllers
{
    public class HomeController : Controller
    {
        MembersDBContext memberDB = new MembersDBContext();
        public ActionResult Index()
        {
            memberDB.Members.FirstOrDefault();

            return View();
        }
    }
}
