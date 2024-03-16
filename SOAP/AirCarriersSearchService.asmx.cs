using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Web.Services;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;
using Newtonsoft.Json;
using SOAP.Models;

namespace SOAP
{
    /// <summary>
    /// Summary description for AirCarriersSearchService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class AirCarriersSearchService : System.Web.Services.WebService
    {
        private Carriers carriers;
        private Carrier c;
        private List<Carrier> returnedCarriers = new List<Carrier>();
        string rawData;
        string dataPath = @"C:\Users\larak\Desktop\IIS\Projekt_Lara\SOAP\Air_carriers.xml";
        string searchedParams;

        [WebMethod]
        public List<Carrier> SearchCarriers(string key, string value)
        {
            //podaci iz API
            using (var webClient = new WebClient())
            {
                webClient.Headers.Add("X-RapidAPI-Key", "3d572af981msh76feb8b882f1476p14410fjsn6e986425424a");
                webClient.Headers.Add("X-RapidAPI-Host", "skyscanner-api.p.rapidapi.com");
                rawData = webClient.DownloadString("https://skyscanner-api.p.rapidapi.com/v3/flights/carriers");
                carriers = JsonConvert.DeserializeObject<Carriers>(rawData);
            }

            //Save XML data to file
            var serializer = new DataContractSerializer(typeof(Carriers));
            using (XmlWriter writer = XmlWriter.Create(dataPath))
            {
                serializer.WriteObject(writer, carriers);
            }

            // Load XML data for search -> xpath
            XDocument doc = XDocument.Load(dataPath);
            searchedParams = "/carriers/carrier[" + key + "=" + "'" + value + "']";
            var result = doc.XPathSelectElements(searchedParams);

            // Convert XElement to Carrier and add to list
            foreach (var item in result)
            {
                using (StringReader reader = new StringReader(item.ToString()))
                {
                    var xmlSerializer = new XmlSerializer(typeof(Carrier));
                    c = (Carrier)xmlSerializer.Deserialize(reader);
                    returnedCarriers.Add(c);
                }
            }

            return returnedCarriers;
        }
    }
}
