using LoginSystemAndNews.Models.Members;
using LoginSystemAndNews.Interfaces;
using System.Collections.Generic;
using System;
using System.Security.Cryptography;
using System.Text;

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

        public Member GetMemberByAccountOrEmail(string account, string email)
        {
            return _memberRepository.GetByAccountOrEmail(account, email);
        }

        public void AddMember(Member member)
        {
            Member passordAndSalt = HashPassword(member.Password);

            member.Password = passordAndSalt.Password;
            member.Salt = passordAndSalt.Salt;

            _memberRepository.Add(member);
        }

        public void UpdateMemberPassword(Member member)
        {
            string accountSalt= GetMemberByAccount(member.Account).Salt;
            Member selfMember = HashPassword(member.Password, accountSalt);

            member.Password = selfMember.Password;
            member.Salt = selfMember.Salt;

            _memberRepository.Update(member);
        }

        public void DeleteMember(string account)
        {
            _memberRepository.Delete(account);
        }

        public Member HashPassword(string password, string accountSalt = "")
        {
            Member member = new Member();

            
            if (accountSalt == "")
            {
                const int saltLength = 8;

                //生成隨機加鹽字符串
                var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()-_+=~`[]{}|:;'<>,.?/";
                var random = new Random();
                var salt = new char[saltLength];
                for (int i = 0; i < saltLength; i++)
                {
                    salt[i] = characters[random.Next(characters.Length)];
                }
                accountSalt = new String(salt);
            }

            //對加鹽密碼做SHA256加密
            HashAlgorithm ha = new SHA256CryptoServiceProvider();
            var saltPassword = password + accountSalt;
            var hash = Encoding.ASCII.GetBytes(saltPassword);
            ha.ComputeHash(hash);
            var hashPassword = BitConverter.ToString(ha.Hash).Replace("-", string.Empty);

            member.Password = hashPassword;
            member.Salt = accountSalt;

            return member;
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
            _loginTimeLogRepository.AddLoginTime(loginTimeLog);
        }

        public void UpdateLogoutTimeLog(LoginTimeLog loginTimeLog)
        {
            _loginTimeLogRepository.UpdateLogoutTime(loginTimeLog);
        }
    }
}