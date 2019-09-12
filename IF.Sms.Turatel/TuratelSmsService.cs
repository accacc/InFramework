using IF.Core.Sms;
using IF.Core.Sms.Interface;
using IF.Core.Xml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace IF.Sms.Turatel
{


    public class TuratelSmsService : IIFSmsOneToManyServiceAsync, IIFSmsManyToManyServiceAsync, IIFSmsStatusServiceAsync,IIFSmsCallbackServiceAsync
    {

        private readonly TuratelSmsClient httpClient;
        private readonly IFSmsSettings settings;

        public TuratelSmsService(TuratelSmsClient httpClient, IFSmsSettings settings)
        {
            this.httpClient = httpClient;
            this.settings = settings;
        }

        //        01 Kullanıcı adı ya da şifre hatalı
        //02 Kredisi yeterli değil
        //03 Geçersiz içerik
        //04 Bilinmeyen SMS tipi
        //05 Hatalı gönderen ismi
        //06 Mesaj metni ya da Alıcı bilgisi girilmemiş
        //07 İçerik uzun fakat Concat özelliği ayarlanmadığından mesaj birleştirilemiyor
        //08 Kullanıcının mesaj göndereceği gateway tanımlı değil ya da şu anda çalışmıyor
        //09 Yanlış tarih formatı.Tarih ddMMyyyyhhmm formatında olmalıdır
        public async Task<IFSmsResponse> SendSmsAsync(IFSmsOneToManyRequest request)
        {
            IFSmsResponse response = new IFSmsResponse();

            try
            {
                var result = await this.SendSmsO2M(request);

                ConvertResponse(response, result);


            }
            catch (Exception ex)
            {
                response.FromException(ex);
            }

            return response;
        }

        public async Task<IFSmsResponse> SendSmsAsync(IFSmsManyToManyRequest request)
        {
            IFSmsResponse response = new IFSmsResponse();

            try
            {
                var result = await this.SendSmsM2M(request);

                ConvertResponse(response, result);
            }
            catch (Exception ex)
            {
                response.FromException(ex);
            }

            return response;
        }

        public async Task<IFSmsStatusResponse> GetSmsStatusAsync(IFSmsStatusRequest request)
        {
            IFSmsStatusResponse response = new IFSmsStatusResponse();

            try
            {
                //string xml = @"<Request>
                //            <Command>36</Command>
                //            <PlatformID>1</PlatformID>
                //            <ChannelCode>583</ChannelCode>
                //            <UserName>soltekzen</UserName>
                //            <PassWord>7804433032</PassWord>
                //            <MessagePacketId>587521462</MessagePacketId>
                //            <Option>1</Option>
                //            </Request>";

                var doc = new XDocument(
                    new XElement("Request",
                        new XElement("Command", "36"),
                        new XElement("PlatformID", "1"),
                        new XElement("ChannelCode", settings.ChannelCode),
                        new XElement("UserName", settings.UserName),
                        new XElement("PassWord", settings.Password),
                        new XElement("MessagePacketId", request.IntegrationId),
                        new XElement("Option", "1")
                    )
                );

                var httpRequestResult = await this.httpClient.PostXmlAsync(doc);



                if (!httpRequestResult.IsSuccess)
                {
                    response.IsSuccess = false;
                    response.ErrorCode = httpRequestResult.Response;
                    return response;
                }

                if (httpRequestResult.Response.Length < 3)
                {
                    response.IsSuccess = false;
                    response.ErrorCode = httpRequestResult.Response;
                    return response;
                }

                bool IsSuccess = httpRequestResult.Response.Substring(0, 3) == "OK|";

                if (!IsSuccess)
                {
                    response.IsSuccess = false;
                    response.ErrorCode = httpRequestResult.Response;
                    return response;
                }

                var responseString = httpRequestResult.Response.Substring(3, httpRequestResult.Response.Length - 3);

                char char30 = (char)30;


                var responseArray = responseString.Split(char30);

                response.Results = new List<IFSmsNumberResult>();

                char char9 = (char)9;

                foreach (var itemString in responseArray)
                {
                    var itemArray = itemString.Split(char9);

                    response.Results.Add(new IFSmsNumberResult { Number = itemArray[0], SentDate = GetDateTime(itemArray[2]), State = GetSmsState(itemArray[1]) });
                }
            }
            catch (Exception ex)
            {

                response.FromException(ex);
            }

            return response;
        }

        public async Task<IFSmsCallbackResponse> GetSmsCallbackAsync(IFSmsCallbackRequest request)
        {
            IFSmsCallbackResponse response = new IFSmsCallbackResponse();


            try
            {


                var doc = new XDocument(
                new XElement("MainReportRoot",
                    new XElement("Command", "24"),
                    new XElement("PlatformID", "1"),
                    new XElement("ChannelCode", settings.ChannelCode),
                    new XElement("UserName", settings.UserName),
                    new XElement("PassWord", settings.Password)
                )
            );

                var httpRequestApplicationsResult = await this.httpClient.PostXmlAsync(doc);

                if (!httpRequestApplicationsResult.IsSuccess)
                {
                    response.IsSuccess = false;
                    response.ErrorCode = httpRequestApplicationsResult.Response;
                    return response;
                }

                bool IsSuccess = httpRequestApplicationsResult.Response.Substring(0, 3) == "OK|";

                if (!IsSuccess)
                {
                    response.IsSuccess = false;
                    response.ErrorCode = httpRequestApplicationsResult.Response;
                    return response;
                }

                var responseString = httpRequestApplicationsResult.Response.Substring(3, httpRequestApplicationsResult.Response.Length - 3);

                var applications = IFXmlSerializer.Deserialize<IFSmsApplicationXmls>(responseString);

                response.List = new List<Core.Sms.IFSmsCallbackXmlMessages>();

                string status = "2";

                if (!String.IsNullOrWhiteSpace(request.MessageStatus)) status = request.MessageStatus;

                foreach (var application in applications.IApplication.Where(a => a.Prefix == request.Prefix).ToList())
                {
                    //string xml = @"<MainReportRoot> 
                    //     <Command>25</Command> 
                    //     <PlatformID>1</PlatformID> 
                    //     <ChannelCode>583</ChannelCode>
                    //     <UserName>soltekzen</UserName>
                    //     <PassWord>7804433032</PassWord>
                    //     <ApplicationID>10462</ApplicationID>
                    //     <Status>2</Status>                         
                    //</MainReportRoot>";


                    //01 Kullanıcı Adı veya Şifre Hatalı
                    //02 Takip no hatalı
                    //03 Takip no boş
                    //04 Gönderim başarısız(Parametrik gönderim ise mesaj metinleri yada numaralar boştur)
                    //05 Takip no’ya ait gönderim bulunamadı ya da şu anda gönderiliyor
                    //07 Takip no’ya ait gönderim bulunamadı 08 Gönderim kullanıcı tarafından iptal edilmiş


                    //9053XXXXXXXXchr(32)3chr(32)20090901180000

                    var callbackDoc = new XDocument(
                new XElement("MainReportRoot",
                    new XElement("Command", "25"),
                    new XElement("PlatformID", "1"),
                    new XElement("ChannelCode", settings.ChannelCode),
                    new XElement("UserName", settings.UserName),
                    new XElement("PassWord", settings.Password),
                    new XElement("ApplicationID", application.ID),
                    new XElement("Status", status)
                )
            );

                    var httpRequestCallbackResult = await this.httpClient.PostXmlAsync(callbackDoc);


                    //02 Hatalı mesaj toplama ID bilgisi
                    //04 Hatalı durum bilgisi
                    //06 Mesaj toplama uygulamasında listelenecek kayıt bulunamadı
                    //09 Hatalı tarih formatı

                    if (httpRequestCallbackResult.Response.Length==2)
                    {
                        response.IsSuccess = false;
                        response.ErrorCode = httpRequestCallbackResult.Response;
                        continue;
                    }

                    if (!httpRequestCallbackResult.IsSuccess)
                    {
                        response.IsSuccess = false;
                        response.ErrorCode = httpRequestCallbackResult.Response;
                        continue;
                    }

                    var callbackSms = IFXmlSerializer.Deserialize<IFSmsCallbackXmlMessages>(httpRequestCallbackResult.Response);

                    response.List.Add(callbackSms);
                }

                return response;

            }
            catch (Exception ex)
            {

                response.FromException(ex);
            }

            return response;
        }












        private async Task<HttpRequestResult> SendSmsO2M(IFSmsOneToManyRequest request)
        {

            string templateMessage = request.Message;

            if (!String.IsNullOrWhiteSpace(request.CallBackMessageTemplate))
            {
                templateMessage = request.Message + " " + String.Format(request.CallBackMessageTemplate, request.CallBackNumberId, request.CallBackPrefixName);
            }

            var doc = new XDocument(
                new XElement("MainmsgBody",
                    new XElement("Command", "0"),
                    new XElement("PlatformID", "1"),
                    new XElement("ChannelCode", settings.ChannelCode),
                    new XElement("UserName", settings.UserName),
                    new XElement("PassWord", settings.Password),
                    new XElement("Type", "1"),
                    new XElement("Concat", "0"),
                    //new XElement("Option", "1"),
                    new XElement("Originator", request.Subject),
                    new XElement("Mesgbody", templateMessage),
                    new XElement("Numbers", string.Join(",", request.Numbers.Select(s=>s.Number))),
                    new XElement("SDate", DatetimeToString(request.StartDate)),
                    new XElement("EDate", DatetimeToString(request.EndDate))
                )
            );


            var response = await httpClient.PostXmlAsync(doc);



            return response;
        }


        private async Task<HttpRequestResult> SendSmsM2M(IFSmsManyToManyRequest request)
        {

            var messageDoc = new XElement("Messages");

            foreach (var message in request.Messages)
            {
                string templateMessage = message.Message;

                if(!String.IsNullOrWhiteSpace(request.CallBackMessageTemplate))
                {
                    templateMessage = message.Message + " " + String.Format(request.CallBackMessageTemplate, request.CallBackNumberId, request.CallBackPrefixName);
                }

                messageDoc.Add(
                    new XElement("Message",
                        new XElement("Mesgbody", templateMessage),
                        new XElement("Number", message.Number)));
            }


            var doc = new XDocument(
               new XElement("MainmsgBody",
                   new XElement("Command", "1"),
                   new XElement("PlatformID", "1"),
                   new XElement("ChannelCode", settings.ChannelCode),
                   new XElement("UserName", settings.UserName),
                   new XElement("PassWord", settings.Password),
                   new XElement("Type", "1"),
                   new XElement("Concat", "0"),
                   //new XElement("Option", "1"),
                   new XElement("Originator", request.Subject),
                   messageDoc,

                    new XElement("SDate", DatetimeToString(request.StartDate)),
                    new XElement("EDate", DatetimeToString(request.EndDate))
               )
           );


            var response = await httpClient.PostXmlAsync(doc);

            return response;
        }

       

        private DateTime? GetDateTime(string date)
        {
            if (String.IsNullOrWhiteSpace(date)) return null;

            return DateTime.ParseExact(date, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
        }

        private BatchItemState GetSmsState(string state)
        {
            int intState = Convert.ToInt32(state);

            if (intState == 3) return BatchItemState.Success;
            if (intState == 5) return BatchItemState.Failed;
            if (intState == 6) return BatchItemState.Waiting;
            if (intState == 9) return BatchItemState.Expired;
            return BatchItemState.Unknown;

        }

        private string DatetimeToString(DateTime? date)
        {
            if (!date.HasValue) return "";

            return date.Value.ToString("yyyy-MM-dd-HH-mm-ss");
        }


        private void ConvertResponse(IFSmsResponse response, HttpRequestResult smsResult)
        {
            if (smsResult.IsSuccess == false)
            {
                response.IsSuccess = false;
                response.ErrorCode = smsResult.Response;
                return;
            }

            if (smsResult.Response.Contains("ID:"))
            {
                response.IsSuccess = true;
                response.IntegrationId = smsResult.Response.Replace("ID", "").Replace(":", "");

            }
            else
            {
                response.IsSuccess = false;
                response.ErrorCode = smsResult.Response;
            }
        }

        
    }
}

