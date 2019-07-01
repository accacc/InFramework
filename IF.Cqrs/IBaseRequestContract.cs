using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Handler
{
    public interface IBaseRequestContract
    {

         string Locale { get; set; }
         string ConversationId { get; set; }

         string UserName { get; set; }

         int UserId { get; set; }

         string ApplicationCode { get; set; }

         string ClientIp { get; set; }

        Guid UniqueId { get; set; }
    }
}
