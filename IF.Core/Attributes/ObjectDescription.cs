using System;

namespace IF.Core.Attributes
{
    public class ObjectDescriptionAttribute : Attribute
    {

        public string Value { get; protected set; }

        public ObjectDescriptionAttribute(string value)
        {
            this.Value = value;
        }

    }
}
