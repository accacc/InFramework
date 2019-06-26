using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Template
{
    public interface ITemplateRenderer
    {
        Task<IFTemplateResponse> GetTemplate(IFTemplateRequest request);
    }
}
