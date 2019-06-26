
namespace Derin.Core.Audit
{
    public interface IAuditableContext
    {
        int SaveChanges();
        //int SaveChanges(SaveOptions saveOptions);
    }
}
