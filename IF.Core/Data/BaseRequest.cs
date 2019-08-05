using IF.Core.Security;
using Newtonsoft.Json;
using System;

namespace IF.Core.Data
{
    public class BaseRequest: IUserIdentity, IBaseRequestContract,IBaseRequest
    {

        public int UserId { get; set; }

        public string UserName { get; set; }


        public string ApplicationCode { get; set; }

        public string Locale { get; set; }
        public string ConversationId { get; set; }

        public string ClientIp { get; set; }
        public string Channel { get; set; }

        public Guid UniqueId { get; set; } = Guid.NewGuid();


    }
}
