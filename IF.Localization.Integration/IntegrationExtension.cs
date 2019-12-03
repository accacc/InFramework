using IF.Core.DependencyInjection.Interface;
using IF.Core.Localization;
using IF.Persistence.EF.Localization;
using IF.Web.Mvc.Localization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace IF.Localization.Integration
{
    public static class IntegrationExtension
    {
        public static ILocalizationBuilder AddEntityFramework(this ILocalizationBuilder builder, IServiceCollection services, LanguageMapper languageMapper, LocalizationSettings settings)
        {

            builder.Container.RegisterInstance(settings, DependencyScope.Single);
            builder.Container.RegisterInstance(languageMapper, DependencyScope.Single);
            builder.Container.RegisterType<LanguageService, ILanguageService>(DependencyScope.PerInstance);
            builder.Container.RegisterType<TranslatorService, ITranslatorService>(DependencyScope.PerInstance);

            var defaultLanguage = settings.Cultures.SingleOrDefault(c => c.LCID == settings.DefaultLanguage);

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.RequestCultureProviders.Clear();
                options.DefaultRequestCulture = new RequestCulture(culture: defaultLanguage.Name, uiCulture: defaultLanguage.Name);
                options.SupportedCultures = settings.Cultures;
                options.SupportedUICultures = settings.Cultures;
                options.RequestCultureProviders.Add(new IFRequestCultureProvider());
            });


            return builder;
        }

        public static IInFrameworkBuilder UseRequestLocalization(this IInFrameworkBuilder builder, IApplicationBuilder app)
        {
            app.UseRequestLocalization();
            return builder;
        }        

    }
}
