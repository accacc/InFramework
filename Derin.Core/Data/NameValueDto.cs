

using Derin.Core.Localization;

namespace Derin.Core.Data
{
    public class NameValueDto : LanguageDTO
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public NameValueDto()
        {

        }
        public NameValueDto(string Value,string Name)
        {
            this.Name = Name;
            this.Value = Value;
        }
    }
}
