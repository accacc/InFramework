using IF.Core.Localization;

namespace IF.Core.Data
{
    public class NameValueDto
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
