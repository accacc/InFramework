namespace IF.Core.Configuration
{
    public interface IConnectionStringReaderService
    {
        string GetConnectionString();

        string GetConnectionString(string dbKey,string serverName);
    }
}
