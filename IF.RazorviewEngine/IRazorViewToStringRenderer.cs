using IF.Core.Email;
using IF.Core.Template;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.RazorviewEngine
{
    public interface IRazorViewToStringRenderer : ITemplateRenderer
    {
        Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model);
    }
}
