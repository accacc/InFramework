using System.Collections.Generic;
using System.Xml.Serialization;

namespace IF.Core.Sms
{
    [XmlRoot(ElementName = "Message")]
    public class IFSmsCallbackXmlMessage
    {
        [XmlElement(ElementName = "MsgID")]
        public string MsgID { get; set; }
        [XmlElement(ElementName = "Date")]
        public string Date { get; set; }
        [XmlElement(ElementName = "Number")]
        public string Number { get; set; }
        [XmlElement(ElementName = "Text")]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "Messages")]
    public class IFSmsCallbackXmlMessages
    {
        [XmlElement(ElementName = "Message")]
        public List<IFSmsCallbackXmlMessage> Message { get; set; }
    }
}
