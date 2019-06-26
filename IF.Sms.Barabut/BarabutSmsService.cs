using IF.Core.Sms;
using IF.Sms.Barabut;
using System;
using System.Collections.Generic;

namespace Derin.SocialAndCommunication.Sms.Integration.InfoBip
{
    public class BarabutSmsService : IIFSmsService
    {

        private string userName;
        private string password;

        public BarabutSmsService(string UserName, string Password, Dictionary<string, string> ServiceNumbers, decimal? MinCreditNumberCount)
        {
            this.userName = UserName;
            this.password = Password;
            //this.serviceNumbers = ServiceNumbers;
            //this.minCreditNumberCount = MinCreditNumberCount;


            //infoBipApiClient = GetHttpClient("https://api.infobip.com", UserName, Password);

        }

        public IFSmsResponse SendSms(IFSmsRequest request)
        {

            IFSmsResponse derinSmsResponse = new IFSmsResponse();

            try
            {

                SmsResult smsResult = MesajGonder(request.Subject, request.Message, request.Numbers, null);

                if (!smsResult.IsSuccess)
                {
                    derinSmsResponse.IsSuccess = false;
                    derinSmsResponse.Code = smsResult.Code.ToString();
                    derinSmsResponse.ErrorMessage = smsResult.Desc;
                }
                else
                {
                    derinSmsResponse.IsSuccess = true;
                    derinSmsResponse.IntegrationId = smsResult.IntegrationId;
                }
            }
            catch (Exception ex)
            {

                derinSmsResponse.IsSuccess = false;
                derinSmsResponse.ErrorMessage = "Sms Api Error : " + ex.GetBaseException().Message;

                return derinSmsResponse;
            }

            return derinSmsResponse;
        }


        private SmsResult MesajGonder(string başlık, string mesaj, List<string> numaralar, DateTime? ileritarih)
        {
            SmsResult smsResult;
            try
            {
                Messenger messenger = new Messenger(this.userName, this.password);
                SubmitResponse submitResponse = messenger.Submit(mesaj, numaralar, new Header()
                {
                    From = başlık,
                    ScheduledDeliveryTime = ileritarih,
                    ValidityPeriod = 0
                }, DataCoding.Default);
                int code = submitResponse.Response.Status.Code;
                string description = submitResponse.Response.Status.Description;
                long messageId = submitResponse.Response.MessageId;

                smsResult = new SmsResult()
                {
                    IsSuccess = (code != 200 ? false : description == "OK"),
                    Code = code,
                    Desc = description,
                    IntegrationId = submitResponse.Response.MessageId.ToString()
                };
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                smsResult = new SmsResult()
                {
                    IsSuccess = false,
                    Code = -1,
                    Desc = exception.Message
                };
            }
            return smsResult;
        }
    }


 
}
