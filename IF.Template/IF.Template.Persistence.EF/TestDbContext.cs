using IF.Template.Persistence.EF.Mappings;
using Microsoft.EntityFrameworkCore;
using System;

namespace IF.Template.Persistence.EF
{
    public class TestDbContext : DbContext
    {

        public TestDbContext(DbContextOptions<TestDbContext> options)
          : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new TestEntityMapping());


        }
    }
}