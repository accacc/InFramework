using IF.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Sms
{
    public class IFSmsManyToManyRequest : BaseRequest
    {
        public string Subject { get; set; }

        public Guid SourceId { get; set; }

        public List<IFSmsManyToManyModel> Messages { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }

    public class IFSmsManyToManyModel
    {
        public string Message { get; set; }

        public string Number { get; set; }
    }



}
