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
        public DbSet<IFNugetPackage> Dlls { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=../../ProjectTemplate.db;");
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IFProjectTemplate>().ToTable("ProjectTemplates");
            modelBuilder.Entity<IFProject>().ToTable("Projects");
            modelBuilder.Entity<IFNugetPackage>().ToTable("NugetPackages");

            modelBuilder.Entity<IFProjectNugetPackage>().ToTable("ProjectNugetPackages");
            modelBuilder.Entity<IFProjectNugetPackage>().Property(s => s.IFNugetPackageId).HasColumnName("NugetPackageId").IsRequired();
            modelBuilder.Entity<IFProjectNugetPackage>().Property(s => s.IFProjectId).HasColumnName("ProjectId").IsRequired();


            modelBuilder.Entity<IFProjectNugetPackage>().HasKey(sc => new { sc.IFNugetPackageId, sc.IFProjectId });

            modelBuilder.Entity<IFProjectNugetPackage>()
                .HasOne<IFProject>(sc => sc.Project)
                .WithMany(s => s.IFProjectNugetPackages)
                .HasForeignKey(sc => sc.IFProjectId);


            modelBuilder.Entity<IFProjectNugetPackage>()
                .HasOne<IFNugetPackage>(sc => sc.NugetPackage)
                .WithMany(s => s.IFProjectNugetPackages)
                .HasForeignKey(sc => sc.IFNugetPackageId);
        }
    }
}
