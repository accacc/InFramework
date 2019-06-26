using IF.Sms.Barabut;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Derin.SocialAndCommunication.Sms.Integration.InfoBip
{
    public class XmlServiceClient : IDisposable
    {
        public string BaseUri
        {
            get;
            set;
        }

        public string Error
        {
            get;
            private set;
        }

        public bool Status
        {
            get;
            private set;
        }

        public TimeSpan? Timeout
        {
            get;
            set;
        }

        public XmlServiceClient(string baseUri)
        {
            this.BaseUri = baseUri;
        }

        public void Dispose()
        {
        }

        public SubmitResponse Send(Submit request)
        {
            SubmitResponse t;

            //string str = System.IO.File.ReadAllText(@"C:\temp\sms.xml"); 


            String str = @"<Submit xmlns=""SmsApi"" xmlns:i=""http://www.w3.org/2001/XMLSchema-instance"">";
            str += "<Credential>";
            str += @"<Password>" + request.Credential.Password + "</Password>";
            str += "<Username>" + request.Credential.Username + "</Username>";
            str += "</Credential>";
            str += "<DataCoding>Default</DataCoding>";
            str += "<Header>";
            str += "<From>" + request.Header.From + "</From>";
            str += @"<ScheduledDeliveryTime i:nil=""true""/>";
            str += "<ValidityPeriod>0</ValidityPeriod>";
            str += "</Header>";
            str += "<Message>" + request.Message + "</Message>";
            str += @"<To xmlns:a=""http://schemas.microsoft.com/2003/10/Serialization/Arrays"">";
            str += "<a:string>" + request.To.First() + "</a:string>";
            str += "</To>";
            str += "</Submit>";


            string str1 = string.Concat(this.BaseUri, "/", request.GetType().Name);

            WebRequest totalMilliseconds = WebRequest.Create(str1);

            try
            {
                totalMilliseconds.Method = "POST";

                if (this.Timeout.HasValue)
                {
                    totalMilliseconds.Timeout = (int)this.Timeout.Value.TotalMilliseconds;
                }

                totalMilliseconds.ContentType = "application/xml";


                StreamWriter streamWriter = new StreamWriter(totalMilliseconds.GetRequestStream());

                try
                {
                    streamWriter.Write(str);
                }
                finally
                {
                    if (streamWriter != null)
                    {
                        ((IDisposable)streamWriter).Dispose();
                    }
                }

                string end = (new StreamReader(totalMilliseconds.GetResponse().GetResponseStream())).ReadToEnd();

                end = end.Replace(@"<SubmitResponse xmlns:i=""http://www.w3.org/2001/XMLSchema-instance"" xmlns=""SmsApi"">", "<SubmitResponse>");

                SubmitResponse t1;

                XmlSerializer serializer = new XmlSerializer(typeof(SubmitResponse));

                using (TextReader reader = new StringReader(end))
                {
                     t1 = (SubmitResponse)serializer.Deserialize(reader);
                }                

                this.Status = true;
                

                t = t1;

                return t;
            }
            catch (AuthenticationException authenticationException1)
            {
                AuthenticationException authenticationException = authenticationException1;
                this.Status = false;
                this.Error = authenticationException.Message;
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                this.Status = false;
                this.Error = exception.Message;
            }
            t = new SubmitResponse();
            return t;
        }

    }
}
