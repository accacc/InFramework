using IF.Core.DependencyInjection.Interface;
using IF.Core.Email;
using IF.Core.SendGrid;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace IF.Email.SendGrid.Extension
{
    public static class Extension
    {
        public static IEmailSenderBuilder AddSendGrid(this IEmailSenderBuilder mailBuilder, IServiceCollection services, SendGridEmailSettings sendGridEmailSettings)
        {

            services.AddHttpClient<IIFEmailService, SendGridApiEmailService>();            
            services.AddSingleton(sendGridEmailSettings);            
            return mailBuilder;
        }


    }


}
