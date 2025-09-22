using Microsoft.EntityFrameworkCore;
using np_project.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace np_project.Data.Context
{
    public class ProjectDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=db_np;Trusted_Connection=true;TrustServerCertificate=True");
        }
    }
}
