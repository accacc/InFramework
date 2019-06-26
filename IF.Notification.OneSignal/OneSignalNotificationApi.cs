using IF.Core.Notification;
using IF.Core.OneSignal;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace IF.Notification.OneSignal
{
    public class OneSignalNotificationApi : INotificationService
    {
        private readonly HttpClient httpClient;
        private readonly OneSignalApiSettings settings;

        public OneSignalNotificationApi(HttpClient httpClient, OneSignalApiSettings settings)
        {
            this.httpClient = httpClient;
            this.settings = settings;
        }

        public async Task<IFNotificationResponse> Notify(IFNotificationRequest request)
        {
            IFNotificationResponse notificationResponse = new IFNotificationResponse();

            try
            {
                
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", this.settings.ApiKey);

                var input = new OneSignalRequest()
                {
                    app_id = this.settings.ApplicationId,
                    include_player_ids = request.DeviceIds,
                    data = request.Data,
                    contents = new Content() { en = request.Message, tr = request.Message },
                    headings = new Content() { en = request.Title, tr = request.Title }
                };

                var requestBody = JsonConvert.SerializeObject(input);

                var content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(this.settings.Url, content);

                notificationResponse.IsSuccess = response.IsSuccessStatusCode;
                notificationResponse.Result = response.Content.ReadAsStringAsync().Result.ToString();
                return notificationResponse;

            }
            catch (System.Exception ex)
            {

                notificationResponse.IsSuccess = false;
                notificationResponse.ErrorMessage = "One Signal Api Error : " + ex.GetBaseException().Message;

                return notificationResponse;
            }

            
          
        }
    }
}
