﻿using System.Collections.Generic;
using System.Xml.Serialization;

namespace IF.Core.Sms
{
    [XmlRoot(ElementName = "IApplication")]
    public class IFSmsApplicationXml
    {
        [XmlElement(ElementName = "ID")]
        public string ID { get; set; }
        [XmlElement(ElementName = "GSMOperator")]
        public string GSMOperator { get; set; }
        [XmlElement(ElementName = "MSISDN")]
        public string MSISDN { get; set; }
        [XmlElement(ElementName = "Prefix")]
        public string Prefix { get; set; }
        [XmlElement(ElementName = "CreditsToCharge")]
        public string CreditsToCharge { get; set; }
        [XmlElement(ElementName = "SDate")]
        public string SDate { get; set; }
        [XmlElement(ElementName = "FDate")]
        public string FDate { get; set; }
        [XmlElement(ElementName = "PushStatus")]
        public string PushStatus { get; set; }
        [XmlElement(ElementName = "PushServiceURL")]
        public string PushServiceURL { get; set; }
        [XmlElement(ElementName = "Status")]
        public string Status { get; set; }
    }

    [XmlRoot(ElementName = "IApplications")]
    public class IFSmsApplicationXmls
    {
        [XmlElement(ElementName = "IApplication")]
        public List<IFSmsApplicationXml> IApplication { get; set; }
    }
}
