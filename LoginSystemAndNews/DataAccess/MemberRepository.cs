using System.Collections.Generic;
using System.Linq;
using System.Data.SQLite;
using System;
using LoginSystemAndNews.Models.Members;
using LoginSystemAndNews.Interfaces;
using LoginSystemAndNews.Helpers;
using System.Security.Cryptography;
using System.Text;

namespace LoginSystemAndNews.DataAccess
{
    public class MemberRepository : IMemberRepository
    {
        public IEnumerable<Member> GetAll()
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection();
            m_dbConnection = new SQLiteConnection("Data Source=|DataDirectory|MembersDB.db; version=3;");
            m_dbConnection.Open();

            string sql = "SELECT * from Members";
            List<Member> memberList = new List<Member>();
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Member memberData = new Member();

                memberData.Account = reader["Account"].ToString();
                memberData.Password = reader["Password"].ToString();
                memberData.Name = reader["Name"].ToString();
                memberData.Email = reader["Email"].ToString();
                memberData.IsAdmin = reader["IsAdmin"].ToString();
                memberData.Salt = reader["Salt"].ToString();
                memberData.RegistTime = (DateTime?)reader["RegistTime"];

                memberList.Add(memberData);
            }

            m_dbConnection.Close();


            return memberList;
        }

        public Member GetByAccount(string account)
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection();
            m_dbConnection = new SQLiteConnection("Data Source=|DataDirectory|MembersDB.db; version=3;");
            m_dbConnection.Open();

            string sql = "SELECT * from Members where Account=@Account";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.Parameters.AddWithValue("@Account", account);
            SQLiteDataReader reader = command.ExecuteReader();
            Member memberData = new Member();
            while (reader.Read())
            {
                memberData.Account = reader["Account"].ToString();
                memberData.Password = reader["Password"].ToString();
                memberData.Name = reader["Name"].ToString();
                memberData.Email = reader["Email"].ToString();
                memberData.IsAdmin = reader["IsAdmin"].ToString();
                memberData.Salt = reader["Salt"].ToString();
                memberData.RegistTime = (DateTime?)reader["RegistTime"];
            }

            m_dbConnection.Close();

            return memberData;
        }

        public Member GetByAccountOrEmail(string account, string email)
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection();
            m_dbConnection = new SQLiteConnection("Data Source=|DataDirectory|MembersDB.db; version=3;");
            m_dbConnection.Open();

            string sql = "SELECT * from Members where Account=@Account OR Email=@Email";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.Parameters.AddWithValue("@Account", account);
            command.Parameters.AddWithValue("@Email", email);
            SQLiteDataReader reader = command.ExecuteReader();
            Member memberData = new Member();
            while (reader.Read())
            {
                memberData.Account = reader["Account"].ToString();
                memberData.Password = reader["Password"].ToString();
                memberData.Name = reader["Name"].ToString();
                memberData.Email = reader["Email"].ToString();
                memberData.IsAdmin = reader["IsAdmin"].ToString();
                memberData.Salt = reader["Salt"].ToString();
                memberData.RegistTime = (DateTime?)reader["RegistTime"];
            }

            m_dbConnection.Close();

            return memberData;
        }

        public void Add(Member member)
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection();
            m_dbConnection = new SQLiteConnection("Data Source=|DataDirectory|MembersDB.db; version=3;");
            m_dbConnection.Open();

            string sql = "INSERT INTO Members(Account, Password, Name, Email, IsAdmin, Salt, RegistTime) VALUES (@Account, @Password, @Name, @Email, @IsAdmin, @Salt, @RegistTime);";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.Parameters.AddWithValue("@Account", member.Account);
            command.Parameters.AddWithValue("@Password", member.Password);
            command.Parameters.AddWithValue("@Name", member.Name);
            command.Parameters.AddWithValue("@Email", member.Email);
            command.Parameters.AddWithValue("@IsAdmin", "0");
            command.Parameters.AddWithValue("@Salt", member.Salt);
            command.Parameters.AddWithValue("@RegistTime", DateTime.Now);
            command.ExecuteNonQuery();

            m_dbConnection.Close();
        }

        public void Update(Member member)
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection();
            m_dbConnection = new SQLiteConnection("Data Source=|DataDirectory|MembersDB.db; version=3;");
            m_dbConnection.Open();

            string sql = "UPDATE Members SET Password = @Password WHERE Account = @Account";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.Parameters.AddWithValue("@Password", member.Password);
            command.Parameters.AddWithValue("@Account", member.Account);
            command.ExecuteNonQuery();

            m_dbConnection.Close();
        }

        public void Delete(string account)
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection();
            m_dbConnection = new SQLiteConnection("Data Source=|DataDirectory|MembersDB.db; version=3;");
            m_dbConnection.Open();

            string sql = "DELETE FROM Members WHERE Account= @Account";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.Parameters.AddWithValue("@Account", account);
            command.ExecuteNonQuery();

            m_dbConnection.Close();
        }

    }
}