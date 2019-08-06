using IF.Template.Persistence.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Template.Persistence.EF.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<IFUser>
    {
        public void Configure(EntityTypeBuilder<IFUser> builder)
        {
            builder.ToTable("IFUsers");
            // Properties
            //  Id is primary key (identity)
            
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(50);            
            builder.Property(x => x.UserName).HasMaxLength(50);
            
        }
    }
}
