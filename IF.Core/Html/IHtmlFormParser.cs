using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Html
{
    public interface IHtmlFormParser
    {
        List<Input> GetInputs(string form);
    }
}
