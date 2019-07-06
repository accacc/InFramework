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
  

    public class TuratelSmsService : IIFSmsOneToManyServiceAsync//, IIFSmsManyToManyServiceAsync
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
                var result = await this.MesajGonder(request.Subject, request.Message, request.Numbers, null);

                response.IsSuccess = result.IsSuccess;
                response.Code = result.Code.ToString();
                response.ErrorMessage = result.Desc;
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

                response.IsSuccess = result.IsSuccess;
                response.Code = result.Code.ToString();
                response.ErrorMessage = result.Desc;
            }
            catch (Exception ex)
            {
                response.FromException(ex);
            }

            return response;
        }



        



       

        public async Task<SmsResult> MesajGonder(string başlık, string mesaj, List<string> numaralar, DateTime? ileritarih)
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
                    new XElement("Option", "1"),
                    new XElement("Originator", başlık),
                    new XElement("Mesgbody", mesaj),
                    new XElement("Numbers", string.Join(",", numaralar)),
                    new XElement("SDate", "")
                )
            );


            var response = await httpClient.Gonder(doc);

            return response;
        }

        //public SmsResult MesajGonderDogrulamKodu(string başlık, string mesaj, string numara)
        //{
        //    var dogrulamaKodu = new Random().Next(100000, 999999);
        //    var doc = new XDocument(
        //        new XElement("MainmsgBody",
        //            new XElement("Command", "0"),
        //            new XElement("PlatformID", "1"),
        //            new XElement("ChannelCode", settings.ChannelCode),
        //            new XElement("UserName", settings.UserName),
        //            new XElement("PassWord", settings.Password),
        //            new XElement("Type", "1"),
        //            new XElement("Concat", "1"),
        //            new XElement("Option", "1"),
        //            new XElement("Originator", başlık),
        //            new XElement("Mesgbody", mesaj.Replace("@kod", dogrulamaKodu.ToString())),
        //            new XElement("Numbers", numara),
        //            new XElement("SDate", "")
        //        )
        //    );

        //    var response = Gonder(doc);
        //    response.EncryptedCode = GetSHA1(dogrulamaKodu.ToString());
        //    return response;
        //}

       

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
                   new XElement("Concat", "1"),
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

