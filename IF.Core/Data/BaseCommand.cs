using IF.Core.Security;
using System;

namespace IF.Core.Data
{
    public class BaseCommand : BaseResponseContract,IUserIdentity,IBaseRequestContract,IBaseCommand
    {

        public string Locale { get; set; }
        public string ConversationId { get; set; }

        public string UserName { get; set; }

        public int UserId { get; set; }

        public string ApplicationCode { get; set; }

        public string ClientIp { get; set; }

        public Guid UniqueId { get; set; } = Guid.NewGuid();

    }
}
