using LoginSystemAndNews.Interfaces;
using LoginSystemAndNews.Models.Members;
using System.Collections.Generic;
using System.Data.SQLite;
using LoginSystemAndNews.Helpers;
using System;
using LoginSystemAndNews.Models.News;

namespace LoginSystemAndNews.DataAccess
{
    public class NewsRepository : INewsRepository
    {
        public IEnumerable<News> GetAll()
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection();
            m_dbConnection = new SQLiteConnection("Data Source=|DataDirectory|NewsDB.db; version=3;");
            m_dbConnection.Open();

            string sql = "SELECT * from News Order by Date DESC";
            List<News> newsList = new List<News>();
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                News newsData = new News();

                newsData.Id = Int32.Parse(reader["Id"].ToString());
                newsData.Title = reader["Title"].ToString();
                newsData.Category = reader["Category"].ToString();
                newsData.Content = reader["Content"].ToString();
                newsData.Date = (DateTime?)reader["Date"];

                newsList.Add(newsData);
            }

            m_dbConnection.Close();


            return newsList;
        }

        public News GetById(int id)
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection();
            m_dbConnection = new SQLiteConnection("Data Source=|DataDirectory|NewsDB.db; version=3;");
            m_dbConnection.Open();

            string sql = "SELECT * from News where Id=@Id";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.Parameters.AddWithValue("@Id", id);
            SQLiteDataReader reader = command.ExecuteReader();
            News newsData = new News();
            while (reader.Read())
            {
                newsData.Id = Int32.Parse(reader["Id"].ToString());
                newsData.Title = reader["Title"].ToString();
                newsData.Category = reader["Category"].ToString();
                newsData.Content = reader["Content"].ToString();
                newsData.Date = (DateTime?)reader["Date"];
            }

            m_dbConnection.Close();

            return newsData;
        }

        public void AddNews(News news)
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection();
            m_dbConnection = new SQLiteConnection("Data Source=|DataDirectory|NewsDB.db; version=3;");
            m_dbConnection.Open();

            string sql = "INSERT INTO News(Title, Category, Content, Date) VALUES (@Title, @Category, @Content, @Date)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.Parameters.AddWithValue("@Title", news.Title);
            command.Parameters.AddWithValue("@Category", news.Category);
            command.Parameters.AddWithValue("@Content", news.Content);
            command.Parameters.AddWithValue("@Date", DateTime.Now);
            command.ExecuteNonQuery();

            m_dbConnection.Close();
        }

        public void DeleteNews(int id)
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection();
            m_dbConnection = new SQLiteConnection("Data Source=|DataDirectory|NewsDB.db; version=3;");
            m_dbConnection.Open();

            string sql = "DELETE FROM News WHERE Id= @Id";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.Parameters.AddWithValue("@Id", id);
            command.ExecuteNonQuery();

            m_dbConnection.Close();
        }

    }
}
