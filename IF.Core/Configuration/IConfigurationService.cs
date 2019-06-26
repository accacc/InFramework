namespace IF.Core.Configuration
{
    public interface IConfigurationService
    {
        TConfig GetValue<TConfig>(string key);
        TConfig GetValue<TConfig>(string key,TConfig defaultValue);

        TSection GetSection<TSection>() where TSection : new();
    }
}
