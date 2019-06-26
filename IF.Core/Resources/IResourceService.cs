namespace IF.Core.Resources
{
    public interface IResourceService
    {
        string GetResource(string key);
        string GetValidation(string key);
    }
}
