using LoginSystemAndNews.Models.Members;
using System.Collections.Generic;

namespace LoginSystemAndNews.Interfaces
{
    public interface IMemberRepository
    {
        IEnumerable<Member> GetAll();
        Member GetByAccount(string account);
        void Add(Member member);
        void Update(Member member);
        void Delete(string account);
    }

    public interface ILoginTimeRepository
    {
        IEnumerable<LoginTimeLog> GetAll();
        void Add(LoginTimeLog loginTimeLog);
    }

    public interface IMemberService
    {
        IEnumerable<Member> GetAllMember();
        Member GetMemberByAccount(string account);
        void AddMember(Member member);
        void UpdateMember(Member member);
        void DeleteMember(string account);
    }

    public interface ILoginTimeLogService
    {
        IEnumerable<LoginTimeLog> GetAllLoginTimeLog();
        void AddLoginTimeLog(LoginTimeLog loginTimeLog);
    }
}
