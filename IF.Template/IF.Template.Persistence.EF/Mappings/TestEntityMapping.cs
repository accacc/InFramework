using IF.Template.Persistence.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Template.Persistence.EF.Mappings
{
    public class TestEntityMapping : IEntityTypeConfiguration<TestEntity>
    {
        public void Configure(EntityTypeBuilder<TestEntity> builder)
        {
            builder.ToTable("Tests");
            
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Desc).HasMaxLength(50);            
            builder.Property(x => x.Name).HasMaxLength(50);
            
        }
    }
}
