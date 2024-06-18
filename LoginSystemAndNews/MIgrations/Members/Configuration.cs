﻿using LoginSystemAndNews.Models.Members;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.SQLite.EF6.Migrations;
using System.Linq;
using System.Web;

namespace LoginSystemAndNews.Migrations.Menber
{
    //Add-Migration Init -ConfigurationTypeName hospital.Migrations.Biobank.Configuration
    public class Configuration : DbMigrationsConfiguration<MembersDBContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.SetSqlGenerator("System.Data.SQLite", new SQLiteMigrationSqlGenerator());
        }

        protected override void Seed(MembersDBContext context)
        {
            //This method will be called after migrating to the latest version.

            //You can use the DbSet<T>.AddOrUpdate() helper extension method
            //to avoid creating duplicate seed data.E.g.
        }
    }
}