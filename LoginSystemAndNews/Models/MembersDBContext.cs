using LoginSystemAndNews.Migrations.Menber;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Web;

namespace LoginSystemAndNews.Models.Members
{
    public class MembersDBContext : DbContext
    {
        public MembersDBContext() : base("MembersDB")
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
                        Database.SetInitializer(new MigrateDatabaseToLatestVersion<MembersDBContext,
                                                    Configuration>());
                        s_migrated[0] = true;
                    }
                }
            }
        }

        public virtual DbSet<Member> Members { set; get; }

        public virtual DbSet<LoginTimeLog> LoginTimeLogs { set; get; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}