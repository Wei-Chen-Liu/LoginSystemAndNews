using LoginSystemAndNews.Interfaces;
using LoginSystemAndNews.Models.Members;
using LoginSystemAndNews.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Net;
using System.Web.Security;

namespace LoginSystemAndNews.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;
        private readonly ILoginTimeLogService _loginTimeLogService;

        MemberHelper membersHelper = new MemberHelper();


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
            return View();
        }

        [HttpPost]
        public string MemberCheck(string account, string password)
        {
            
            Member member = _memberService.GetMemberByAccount(account);

            if(member.Account != null)
            {
                string sqlHashPassword = _memberService.HashPassword(password, member.Salt).Password;

                if(member.Password == sqlHashPassword)
                {
                    DateTime nowTime = DateTime.Now;

                    Member memberData = new Member();

                    memberData.Account = member.Account;
                    memberData.Password = member.Password;
                    memberData.Name = member.Name;
                    memberData.Email = member.Email;
                    memberData.IsAdmin = member.IsAdmin;

                    //將物件序列化成字串
                    string userData = JsonConvert.SerializeObject(memberData);

                    //副程式SetAuthenTicket 創立一張驗證票跟存入Cookie
                    membersHelper.SetAuthenTicket(userData, member.Name);//使用者資訊和使用者名稱

                    //產生Cookie
                    HttpCookie cookieAccount = new HttpCookie("Account");
                    HttpCookie cookieName = new HttpCookie("Name");
                    HttpCookie loginTime = new HttpCookie("Login_time");

                    //設定單值
                    cookieAccount.Value = Uri.EscapeDataString(member.Account);
                    cookieName.Value = Uri.EscapeDataString(member.Name);
                    loginTime.Value = nowTime.ToString("yyyy/MM/dd HH:mm:ss.fffffff");

                    //設定過期日
                    cookieAccount.Expires = DateTime.Now.AddHours(2);
                    cookieName.Expires = DateTime.Now.AddHours(2);
                    loginTime.Expires = DateTime.Now.AddHours(2);

                    //寫到用戶端
                    Response.Cookies.Add(cookieAccount);
                    Response.Cookies.Add(cookieName);
                    Response.Cookies.Add(loginTime);

                    LoginTimeLog loginTimeLog = new LoginTimeLog();

                    loginTimeLog.Account = member.Account;
                    loginTimeLog.LoginTime = nowTime;

                    _loginTimeLogService.AddLoginTimeLog(loginTimeLog);

                    return "OK";
                }
                else
                {
                    return "帳號密碼錯誤";
                }
            }
            else
            {
                return "帳號密碼錯誤";
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public string MemberRegistCheck(string account, string email)
        {

            Member member = _memberService.GetMemberByAccountOrEmail(account, email);

            if(member.Account == null && member.Email == null)
            {
                return "OK";
            }
            else if(member.Account != null && member.Account == account && member.Email != email)
            {
                return "帳號已被使用";
            }
            else if(member.Email != null && member.Email == email && member.Account != account)
            {
                return "Email已被使用";
            }
            else
            {
                return "帳號及Email已被使用";
            }

        }
        public ActionResult MemberRegist(FormCollection formData)
        {

            Member member = new Member();
            member.Account = formData["account-input"];
            member.Password = formData["password-input"];
            member.Name = formData["name-input"];
            member.Email = formData["email-input"];

            _memberService.AddMember(member);


            return RedirectToAction("SignIn");
        }

        public ActionResult ForgetPassword()
        {
            return View();
        }

        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public string ResetNewPassword(string account, string password, string verifyCode)
        {
            string verifyCodeTest = "QAZWSX123";
            
            if(verifyCode != verifyCodeTest)
            {
                return "驗證碼錯誤，請重新輸入";
            }
            Member member = new Member();
            member.Account = account;
            member.Password = password;

            _memberService.UpdateMemberPassword(member);

            return "OK";
        }

        [HttpPost]
        public string SignOut()
        {

            HttpCookie nameCookie = Request.Cookies["Name"];
            HttpCookie accountCookie = Request.Cookies["Account"];
            HttpCookie loginTime = Request.Cookies["Login_time"];

            if (User.Identity.Name != "")
            {
                var name = User.Identity.Name;
                HttpContext.Response.Cookies["Name"].Expires = DateTime.Now.AddDays(-1);
                HttpContext.Response.Cookies["Account"].Expires = DateTime.Now.AddDays(-1);
                HttpContext.Response.Cookies["Login_time"].Expires = DateTime.Now.AddDays(-1);
                FormsAuthentication.SignOut();
            }
            else
            {
                HttpContext.Response.Cookies["Name"].Expires = DateTime.Now.AddDays(-1);
                HttpContext.Response.Cookies["Account"].Expires = DateTime.Now.AddDays(-1);
                HttpContext.Response.Cookies["Login_time"].Expires = DateTime.Now.AddDays(-1);
                FormsAuthentication.SignOut();
            }
            LoginTimeLog loginTimeLog = new LoginTimeLog();
            var logintimeToDateTime = DateTime.Parse(loginTime.Value);

            loginTimeLog.Account = accountCookie.Value;
            loginTimeLog.LoginTime = logintimeToDateTime;

            _loginTimeLogService.UpdateLogoutTimeLog(loginTimeLog);

            return "OK";
        }
    }
}
