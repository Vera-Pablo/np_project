using Microsoft.EntityFrameworkCore;
using np_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace np_project.Data
{
    public class ProjectDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options){ }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Dni)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
