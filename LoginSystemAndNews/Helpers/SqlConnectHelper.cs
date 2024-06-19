using System;
using System.Data.SQLite;

namespace LoginSystemAndNews.Helpers
{
    public class SqlConnectHelper
    {
        public void ConnectToDatabase(SQLiteConnection m_dbConnection)
        {
            m_dbConnection = new SQLiteConnection("Data Source=|DataDirectory|MembersDB.db; version=3;");
            m_dbConnection.Open();
        }

        public void DatabaseClose(SQLiteConnection m_dbConnection)
        {
            m_dbConnection.Close();
        }
    }
}