using IF.Core.DependencyInjection.Interface;
using IF.Core.Localization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Persistence.EF.Localization
{
    public static class IntegrationExtension
    {
        public static ILocalizationBuilder AddEntityFramework(this ILocalizationBuilder builder,LocalizationSettings settings)
        {

            builder.Container.RegisterInstance(settings, DependencyScope.Single);            
            builder.Container.RegisterType<LanguageService,ILanguageService>(DependencyScope.PerInstance);
            builder.Container.RegisterType<TranslatorService, ITranslatorService>(DependencyScope.PerInstance);
            
            return builder;
        }
    }
}
