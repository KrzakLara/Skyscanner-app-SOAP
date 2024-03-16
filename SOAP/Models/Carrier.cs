using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SOAP.Models
{
    [XmlRoot("carrier")]
    [DataContract]
    public class Carrier
    {
        public Carrier()
        {
        }

        public Carrier(string name, string iATA)
        {
            Name = name;
            IATA = iATA;
        }

        [DataMember(Name = "name")]
        [XmlElement("name")]
        public string Name { get; set; }

        [DataMember(Name = "iata")]
        [XmlElement("iata")]
        public string IATA { get; set; }
    }
}
