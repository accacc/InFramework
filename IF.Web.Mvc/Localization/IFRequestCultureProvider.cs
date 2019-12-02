using IF.Core.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace IF.Web.Mvc.Localization
{
    public class IFRequestCultureProvider : RequestCultureProvider
    {

        public IFRequestCultureProvider()
        {

        }
        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {

            var languageService = httpContext.RequestServices.GetService<ILanguageService>();

            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            var lcid = 0;

            Int32.TryParse(httpContext.Request.Query["Locale"], out lcid);




            if (lcid == 0)
            {
                return Task.FromResult<ProviderCultureResult>(null);
            }

            var culture = languageService.Cultures.SingleOrDefault(c => c.LCID == lcid).Name;


            return Task.FromResult(new ProviderCultureResult(culture));



        }
    }
}
