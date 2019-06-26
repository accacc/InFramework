using Derin.Core.Configuration;
using Derin.Core.Email;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;

namespace Derin.Core.Exception
{

    public class DiagnosticMail : IDiagnosticMail
    {
        private readonly ISimpleMailSender mailSender;
        private readonly IAppSettings appSettings;
        public DiagnosticMail(ISimpleMailSender mailSender, IAppSettings appSettings)
        {
            this.mailSender = mailSender;
            this.appSettings = appSettings;
        }

        public void SendMail(System.Exception exception, object parameter, string userId, string extraInfo = "")
        {


            StringBuilder executionInfo = new StringBuilder();

            Type commandType = parameter.GetType();

            executionInfo.AppendLine("OPERATION NAME : " + commandType.FullName);

            executionInfo.AppendLine("<-----------PARAMETERS--------------->");

            foreach (var property in commandType.GetProperties())
            {
                executionInfo.AppendLine(property.Name + ":" + property.GetValue(parameter));
            }

            executionInfo.AppendLine("<-----------PARAMETERS--------------->");

            if (!String.IsNullOrWhiteSpace(userId))
            {
                executionInfo.AppendLine("USER ID : " + userId);
            }



            var errorMailList = appSettings.ApplicationErrorMailList;

            var dbName = ConfigurationManager.AppSettings["DbKey"];

            string userName = "";
            try
            {
                userName = System.Threading.Thread.CurrentPrincipal.Identity.Name.Split(',').First();
            }
            catch (System.Exception)
            {

                userName = Environment.UserName;
            }

            string message = extraInfo + GetExceptionDetails(exception);

            string body = String.Empty;

            body += "MACHINE NAME      : " + Environment.MachineName + Environment.NewLine;
            body += "DATABASE NAME     : " + dbName + Environment.NewLine;
            body += "USER ID           : " + userName + Environment.NewLine;
            body += Environment.NewLine;
            body += message;

            string subject = "Hata Oluştu: " + DateTime.Now.ToLongDateString();

            string from = this.appSettings.ApplicationErrorSender;

            this.mailSender.SendMail(subject, body, from, errorMailList.Split(';').ToList());


        }

        private static string GetExceptionDetails(System.Exception exception)
        {

            while (exception.InnerException != null)
                exception = exception.InnerException;

            return "Exception: " + exception.GetType()
                + "\r\nMessage: " + exception.Message
                + "\r\nStackTrace: " + exception.StackTrace;
        }
    }
}

