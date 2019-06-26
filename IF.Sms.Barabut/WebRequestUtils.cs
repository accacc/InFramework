using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Text;

namespace Derin.SocialAndCommunication.Sms.Integration.InfoBip
{
    internal static class WebRequestUtils
    {
        internal static AuthenticationException CreateCustomException(string uri, AuthenticationException ex)
        {
            AuthenticationException authenticationException;
            if (!uri.StartsWith("https"))
            {
                authenticationException = null;
            }
            else
            {
                authenticationException = new AuthenticationException(string.Format("Invalid remote SSL certificate, overide with: \nServicePointManager.ServerCertificateValidationCallback += ((sender, certificate, chain, sslPolicyErrors) => isValidPolicy);", new object[0]), ex);
            }
            return authenticationException;
        }
    }
}
