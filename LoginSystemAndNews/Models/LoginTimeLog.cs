using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace LoginSystemAndNews.Models.Members
{
    public class LoginTimeLog 
    {

        /// <summary>
        /// 流水號
        /// </summary>
        [Key]
        public int Index { get; set; }

        public string Account { get; set; }

        public DateTime? LoginTime { get; set; }

        public DateTime? LogoutTime { get; set; }

    }
}
