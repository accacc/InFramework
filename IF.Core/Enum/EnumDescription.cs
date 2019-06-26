using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Enum
{
    public class EnumDescription : Attribute
    {
        public string Text { get; private set; }

        public EnumDescription(string text)
        {
            this.Text = text;
        }
    }
}
