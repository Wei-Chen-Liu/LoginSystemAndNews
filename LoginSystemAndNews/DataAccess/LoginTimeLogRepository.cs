using LoginSystemAndNews.Interfaces;
using LoginSystemAndNews.Models.Members;
using System.Collections.Generic;
using System.Data.SQLite;
using LoginSystemAndNews.Helpers;
using System;

namespace LoginSystemAndNews.DataAccess
{
    public class LoginTimeLogRepository : ILoginTimeRepository
    {
        public IEnumerable<LoginTimeLog> GetAll()
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection();
            m_dbConnection = new SQLiteConnection("Data Source=|DataDirectory|MembersDB.db; version=3;");
            m_dbConnection.Open();

            string sql = "SELECT * from LoginTimeLogs";
            List<LoginTimeLog> loginTimeLogList = new List<LoginTimeLog>();
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                LoginTimeLog loginTimeLog = new LoginTimeLog();

                loginTimeLog.Account = reader["Account"].ToString();
                loginTimeLog.LoginTime = (DateTime?)reader["LoginTime"];
                loginTimeLog.LogoutTime = (DateTime?)reader["LogoutTime"];

                loginTimeLogList.Add(loginTimeLog);
            }

            m_dbConnection.Close();


            return loginTimeLogList;
        }

        public void AddLoginTime(LoginTimeLog loginTimeLog)
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection();
            m_dbConnection = new SQLiteConnection("Data Source=|DataDirectory|MembersDB.db; version=3;");
            m_dbConnection.Open();

            string sql = "INSERT INTO LoginTimeLogs(Account, LoginTime, LogoutTime) VALUES (@Account, @LoginTime, @LogoutTime)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.Parameters.AddWithValue("@Account", loginTimeLog.Account);
            command.Parameters.AddWithValue("@LoginTime", loginTimeLog.LoginTime);
            command.Parameters.AddWithValue("@LogoutTime", null);
            command.ExecuteNonQuery();

            m_dbConnection.Close();
        }

        public void UpdateLogoutTime(LoginTimeLog loginTimeLog)
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection();
            m_dbConnection = new SQLiteConnection("Data Source=|DataDirectory|MembersDB.db; version=3;");
            m_dbConnection.Open();

            string sql = "UPDATE LoginTimeLogs SET LogoutTime = @LogoutTime WHERE Account = @Account AND LoginTime = @LoginTime";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.Parameters.AddWithValue("@Account", loginTimeLog.Account);
            command.Parameters.AddWithValue("@LoginTime", loginTimeLog.LoginTime);
            command.Parameters.AddWithValue("@LogoutTime", DateTime.Now);
            command.ExecuteNonQuery();

            m_dbConnection.Close();
        }

    }
}
