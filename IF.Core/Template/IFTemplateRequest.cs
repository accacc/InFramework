using IF.Core.Handler;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Template
{
    public class IFTemplateRequest:BaseRequest
    {
        public string ModelName { get; set; }

        public object @object { get; set; }

        public string TemplateName { get; set; }

        public string ModuleName { get; set; }
    }
}
