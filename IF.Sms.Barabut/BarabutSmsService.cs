using IF.Core.Sms;
using IF.Core.Sms.Interface;
using IF.Sms.Barabut;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IF.SocialAndCommunication.Sms.Integration.InfoBip
{
    public class BarabutSmsService : IIFSmsOneToManyService
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

        public IFSmsResponse SendSms(IFSmsOneToManyRequest request)
        {

            IFSmsResponse smsResponse = new IFSmsResponse();

            try
            {

                SmsResult smsResult = MesajGonder(request.Subject, request.Message, request.Numbers.Select(s=>s.Number).ToList(), null);

                if (!smsResult.IsSuccess)
                {
                    smsResponse.IsSuccess = false;
                    smsResponse.Code = smsResult.Code.ToString();
                    smsResponse.ErrorMessage = smsResult.Desc;
                }
                else
                {
                    smsResponse.IsSuccess = true;
                    smsResponse.IntegrationId = smsResult.IntegrationId;
                }
            }
            catch (Exception ex)
            {

                smsResponse.IsSuccess = false;
                smsResponse.ErrorMessage = "Sms Api Error : " + ex.GetBaseException().Message;

                return smsResponse;
            }

            return smsResponse;
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
