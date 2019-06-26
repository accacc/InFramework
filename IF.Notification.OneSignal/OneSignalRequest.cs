using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Notification.OneSignal
{
    public class OneSignalRequest
    {
        public string app_id { get; set; }
        public string[] include_player_ids { get; set; }
        public object data { get; set; }
        public Content contents { get; set; }
        public Content headings { get; set; }
    }

    public class Content
    {
        public string en { get; set; }
        public string tr { get; set; }
    }
}
