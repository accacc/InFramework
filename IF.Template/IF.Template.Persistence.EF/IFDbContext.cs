using IF.Template.Persistence.EF.Mappings;
using Microsoft.EntityFrameworkCore;
using System;

namespace IF.Template.Persistence.EF
{
    public class IFDbContext : DbContext
    {

        public IFDbContext(DbContextOptions<IFDbContext> options)
          : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserMapping());


        }
    }
}