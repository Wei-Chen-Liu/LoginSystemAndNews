using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LoginSystemAndNews.Models.Members;
using Microsoft.Owin.Security;
using Newtonsoft.Json;

namespace LoginSystemAndNews.Helpers
{
    public class MemberHelper
    {
        public void SetAuthenTicket(string userData, string userId)
        {
            //宣告一個驗證票
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
            userId,
            DateTime.Now,
            DateTime.Now.AddHours(2),
            false,
            userData);

            //加密驗證票
            string encryptedTicket = FormsAuthentication.Encrypt(ticket);

            //建立Cookie
            HttpCookie authenticationcookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            authenticationcookie.Expires = DateTime.Now.AddHours(2);

            //將Cookie寫入回應
            HttpContext.Current.Response.Cookies.Add(authenticationcookie);
        }


        //解析SetAuthenTicket中userData的資料
        public Member GetAuthenTicketUserData(string encryptedTicket)
        {
            Member userData = new Member();
            if (!string.IsNullOrEmpty(encryptedTicket))
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(encryptedTicket);
                if (authTicket != null && authTicket.Expired == false)
                {
                    userData = JsonConvert.DeserializeObject<Member>(authTicket.UserData);
                }
            }

            return userData;
        }

        
    }
}
