using Derin.Core.Data;


namespace Derin.Core.Localization
{
    public class SystemLanguage : Entity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public bool Active { get; set; }
    }
}
