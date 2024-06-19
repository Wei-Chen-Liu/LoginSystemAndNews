using System.Collections.Generic;
using System.Linq;
using System.Data.SQLite;
using System;
using LoginSystemAndNews.Models.Members;
using LoginSystemAndNews.Interfaces;

namespace LoginSystemAndNews.DataAccess
{
    public class MemberRepository : IMemberRepository
    {
        public IEnumerable<Member> GetAll()
        {
            List<Member> memberList = new List<Member>();

            return memberList;
        }

        public Member GetByAccount(string account)
        {
            Member member = new Member();


            return member;
        }

        public void Add(Member member)
        {

        }

        public void Update(Member member)
        {
            
        }

        public void Delete(string account)
        {
            
        }

    }
}