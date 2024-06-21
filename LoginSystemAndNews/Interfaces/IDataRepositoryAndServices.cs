using LoginSystemAndNews.Models.Members;
using LoginSystemAndNews.Models.News;
using System.Collections.Generic;

namespace LoginSystemAndNews.Interfaces
{
    public interface IMemberRepository
    {
        IEnumerable<Member> GetAll();
        Member GetByAccount(string account);
        Member GetByAccountOrEmail(string account, string email);
        Member GetByEmail(string email);
        void Add(Member member);
        void Update(Member member);
        void Delete(string account);
    }

    public interface ILoginTimeRepository
    {
        IEnumerable<LoginTimeLog> GetAll();
        void AddLoginTime(LoginTimeLog loginTimeLog);

        void UpdateLogoutTime(LoginTimeLog loginTimeLog);

        LoginTimeLog LoginOrNot(string account, string loginTime);
    }

    public interface INewsRepository
    {
        IEnumerable<News> GetAll();
        News GetById(int id);
        void AddNews(News news);

        void DeleteNews(int id);
    }

    public interface IMemberService
    {
        IEnumerable<Member> GetAllMember();
        Member GetMemberByAccount(string account);
        Member GetMemberByAccountOrEmail(string account, string email);
        Member GetMemberByEmail(string email);
        void AddMember(Member member);
        void UpdateMemberPassword(Member member);
        void DeleteMember(string account);

        Member HashPassword(string password, string accountSalt = "");
    }

    public interface ILoginTimeLogService
    {
        IEnumerable<LoginTimeLog> GetAllLoginTimeLog();
        void AddLoginTimeLog(LoginTimeLog loginTimeLog);

        void UpdateLogoutTimeLog(LoginTimeLog loginTimeLog);

        string LoginOrNot(string account, string loginTime);
    }

    public interface INewsService
    {
        IEnumerable<News> GetAllNews();
        News GetNewsById(int id);
        void AddNews(News news);
        void DeleteNews(int id);
    }
}
