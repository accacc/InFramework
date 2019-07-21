using IF.Core.Sms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IF.Sms.Turatel
{
  

    public class TuratelSmsService : IIFSmsOneToManyServiceAsync,IIFSmsManyToManyServiceAsync
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
        public async Task<IFSmsResponse> SendSmsAsync(IFSmsOnetoManyRequest request)
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


        public void ConvertResponse(IFSmsResponse response,SmsResult smsResult)
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
        



       

        public async Task<SmsResult> SendSmsO2M(IFSmsOnetoManyRequest request)
        {
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
                    new XElement("Mesgbody",request.Message),
                    new XElement("Numbers", string.Join(",", request.Numbers)),
                    new XElement("SDate", "")
                )
            );


            var response = await httpClient.Gonder(doc);

            return response;
        }

     
       

        public async Task<SmsResult> SendSmsM2M(IFSmsManyToManyRequest request)
        {

            var messageDoc = new XElement("Messages");

            foreach (var message in request.Messages)
            {
                messageDoc.Add(
                    new XElement("Message",
                        new XElement("Mesgbody", message.Message),
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

                   new XElement("SDate", "")
               )
           );            


            var response = await httpClient.Gonder(doc);

            return response;
        }

       
    }
}

