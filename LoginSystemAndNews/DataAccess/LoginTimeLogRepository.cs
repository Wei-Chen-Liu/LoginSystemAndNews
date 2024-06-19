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
        SqlConnectHelper sqlConnectHelper = new SqlConnectHelper();
        public IEnumerable<LoginTimeLog> GetAll()
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection();
            sqlConnectHelper.ConnectToDatabase(m_dbConnection);

            string sql = "SELECT * from LoginTimeLog";
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

            sqlConnectHelper.DatabaseClose(m_dbConnection);


            return loginTimeLogList;
        }

        public void AddLoginTime(LoginTimeLog loginTimeLog)
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection();
            sqlConnectHelper.ConnectToDatabase(m_dbConnection);

            string sql = "INSERT INTO LoginTimeLog(Account, LoginTime, LogoutTime) VALUES (@Account, @LoginTime, @LogoutTime)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.Parameters.AddWithValue("@Account", loginTimeLog.Account);
            command.Parameters.AddWithValue("@LoginTime", DateTime.Now);
            command.Parameters.AddWithValue("@LogoutTime", null);
            command.ExecuteReader();

            sqlConnectHelper.DatabaseClose(m_dbConnection);
        }

        public void UpdateLogoutTime(LoginTimeLog loginTimeLog)
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection();
            sqlConnectHelper.ConnectToDatabase(m_dbConnection);

            string sql = "UPDATE LoginTimeLog SET LogoutTime = @LogoutTime WHERE Account = @Account AND LoginTime = @LoginTime";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.Parameters.AddWithValue("@Account", loginTimeLog.Account);
            command.Parameters.AddWithValue("@LoginTime", loginTimeLog.LoginTime);
            command.Parameters.AddWithValue("@LogoutTime", DateTime.Now);
            command.ExecuteReader();

            sqlConnectHelper.DatabaseClose(m_dbConnection);
        }

    }
}
