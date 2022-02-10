using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp30
{
    internal class DB : DbContext
    {
        public DbSet<record> BloodServices { get; set; }

        public DB()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
            stringBuilder.DataSource = @"192.168.1.14\sqlexpress";
            stringBuilder.UserID = "student";
            stringBuilder.Password = "student";
            stringBuilder.InitialCatalog = "1135_blood";
            optionsBuilder.UseSqlServer(stringBuilder.ToString());
            base.OnConfiguring(optionsBuilder);
        }
    }
}
