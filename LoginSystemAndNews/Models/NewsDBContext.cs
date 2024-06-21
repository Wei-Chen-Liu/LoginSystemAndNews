using LoginSystemAndNews.Migrations.News;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Web;
using LoginSystemAndNews.Models.News;

namespace LoginSystemAndNews.Models.News
{
    public class NewsDbContext : DbContext
    {
        public NewsDbContext() : base("NewsDB")
        {
            Migrate();
        }

        private static readonly bool[] s_migrated = { false };

        private static void Migrate()
        {
            if (!s_migrated[0])
            {
                lock (s_migrated)
                {
                    if (!s_migrated[0])
                    {
                        Database.SetInitializer(new MigrateDatabaseToLatestVersion<NewsDbContext,
                                                    Configuration>());
                        s_migrated[0] = true;
                    }
                }
            }
        }

        public virtual DbSet<News> News { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}