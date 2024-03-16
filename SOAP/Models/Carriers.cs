using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SOAP.Models
{
    [DataContract]
    [XmlRoot("carriers")]
    public class Carriers
    {
        public Carriers()
        {
        }

        public Carriers(string status, Dictionary<string, Carrier> carrierDictionary, object allCarriers)
        {
            Status = status;
            CarrierDictionary = carrierDictionary;
            AllCarriers = allCarriers;
        }

        [DataMember(Name = "status")]
        [XmlElement("status")]
        public string Status { get; set; }

        [DataMember(Name = "carriers")]
        [XmlElement("carrier")]
        public Dictionary<string, Carrier> CarrierDictionary { get; set; }

        [XmlIgnore]
        public object AllCarriers { get; internal set; }
    }
}
