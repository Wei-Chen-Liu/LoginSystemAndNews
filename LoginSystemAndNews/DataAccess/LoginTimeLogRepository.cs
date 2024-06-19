using LoginSystemAndNews.Interfaces;
using LoginSystemAndNews.Models.Members;
using System.Collections.Generic;

namespace LoginSystemAndNews.DataAccess
{
    public class LoginTimeLogRepository : ILoginTimeRepository
    {
        public IEnumerable<LoginTimeLog> GetAll()
        {
            List<LoginTimeLog> loginTimeLogList = new List<LoginTimeLog>();

            return loginTimeLogList;
        }

        public void Add(LoginTimeLog loginTimeLog)
        {

        }

    }
}
