using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.Tools.Templates.Editor
{
    public class MyDbContext: DbContext
    {
        public DbSet<IFProjectTemplate> ProjectTemplates { get; set; }
        public DbSet<IFProject> Projects { get; set; }
        public DbSet<IFDll> Dlls { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=../../ProjectTemplate.db;");
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IFProjectTemplate>().ToTable("ProjectTemplates");
            modelBuilder.Entity<IFProject>().ToTable("Projects");
            modelBuilder.Entity<IFDll>().ToTable("Dlls");
        }
    }
}
