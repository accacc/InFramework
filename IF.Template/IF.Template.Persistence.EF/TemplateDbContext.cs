using IF.Template.Persistence.EF;
using Microsoft.EntityFrameworkCore;

public class TemplateDbContext:DbContext{


public  DbSet<User> User { get; set; }
public  DbSet<Address> Address { get; set; }
public   TemplateDbContext (DbContextOptions<TemplateDbContext> options)  : base (options)
{



}

protected override void OnModelCreating (ModelBuilder builder) 
{


builder.ApplyConfiguration(new UserMapping());
builder.ApplyConfiguration(new AddressMapping());

}



}

