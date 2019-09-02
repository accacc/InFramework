using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Sms
{
    public class IFBatchItemModel
    {

        public IFBatchItemModel()
        {
            this.State = BatchItemState.Unknown;
            this.CreatedDate = DateTime.Now;
            this.UpdatedDate = DateTime.Now;
        }
        public BatchItemState State { get; set; }
        public Dictionary<string,string> Parameters { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }



    }
}
