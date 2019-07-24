//using System.Collections.Generic;
//using System.Xml.Serialization;

//namespace IF.Sms.Turatel
//{
//    [XmlRoot(ElementName = "IApplication")]
//    public class TurarelSmsApplicationXml
//    {
//        [XmlElement(ElementName = "ID")]
//        public string ID { get; set; }
//        [XmlElement(ElementName = "GSMOperator")]
//        public string GSMOperator { get; set; }
//        [XmlElement(ElementName = "MSISDN")]
//        public string MSISDN { get; set; }
//        [XmlElement(ElementName = "Prefix")]
//        public string Prefix { get; set; }
//        [XmlElement(ElementName = "CreditsToCharge")]
//        public string CreditsToCharge { get; set; }
//        [XmlElement(ElementName = "SDate")]
//        public string SDate { get; set; }
//        [XmlElement(ElementName = "FDate")]
//        public string FDate { get; set; }
//        [XmlElement(ElementName = "PushStatus")]
//        public string PushStatus { get; set; }
//        [XmlElement(ElementName = "PushServiceURL")]
//        public string PushServiceURL { get; set; }
//        [XmlElement(ElementName = "Status")]
//        public string Status { get; set; }
//    }

//    [XmlRoot(ElementName = "IApplications")]
//    public class TurarelSmsApplicationXmls
//    {
//        [XmlElement(ElementName = "IApplication")]
//        public List<TurarelSmsApplicationXml> IApplication { get; set; }
//    }
//}
