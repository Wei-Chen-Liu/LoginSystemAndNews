using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace LoginSystemAndNews.Models.Members
{
    public class Member 
    {
        [Key]
        public string Account { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Email{ get; set; }

        public string IsAdmin { get; set; }

        public string Salt { get; set; }

        public DateTime? RegistTime { get; set; }

    }
}
