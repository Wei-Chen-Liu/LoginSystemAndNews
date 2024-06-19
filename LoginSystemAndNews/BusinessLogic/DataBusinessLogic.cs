using LoginSystemAndNews.Models.Members;
using LoginSystemAndNews.Interfaces;
using System.Collections.Generic;

namespace LoginSystemAndNews.BusinessLogic
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public IEnumerable<Member> GetAllMember()
        {
            return _memberRepository.GetAll();
        }

        public Member GetMemberByAccount(string account)
        {
            return _memberRepository.GetByAccount(account);
        }

        public void AddMember(Member member)
        {
            _memberRepository.Add(member);
        }

        public void UpdateMember(Member member)
        {
            _memberRepository.Update(member);
        }

        public void DeleteMember(string account)
        {
            _memberRepository.Delete(account);
        }
    }

    public class LoginTimeLogService : ILoginTimeLogService
    {
        private readonly ILoginTimeRepository _loginTimeLogRepository;

        public LoginTimeLogService(ILoginTimeRepository loginTimeRepository)
        {
            _loginTimeLogRepository = loginTimeRepository;
        }


        public IEnumerable<LoginTimeLog> GetAllLoginTimeLog()
        {
            return _loginTimeLogRepository.GetAll();
        }

        public void AddLoginTimeLog(LoginTimeLog loginTimeLog)
        {
            _loginTimeLogRepository.Add(loginTimeLog);
        }
    }
}