
namespace IF.Core.Audit
{
    public interface IAuditableContext
    {
        int SaveChanges();
        //int SaveChanges(SaveOptions saveOptions);
    }


    public interface IAuditContext
    {
        int SaveChanges(bool IsOk);
        //int SaveChanges(SaveOptions saveOptions);
    }
}
