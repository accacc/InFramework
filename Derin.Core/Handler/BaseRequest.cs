using Derin.Core.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Handler
{
    public class BaseRequest: IUserIdentity
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public string LanguageCode { get; set; }

        public string ApplicationCode { get; set; }

        public string Locale { get; set; }
        public string ConversationId { get; set; }


    }
}
