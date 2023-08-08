
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

namespace Notissimus
{
    public class HttpController
    {
        private HttpClient _httpClient;
        private string _url;
        public HttpController(string url)
        {
            _httpClient = new HttpClient();
            _url = url;
        }

        public async Task<List<string>> GetOfferIds()
        {
            List<string> offerIds = new List<string>();

            XmlDocument xmlDoc = await GetAllOfferInfo();

            XmlNodeList offerNodes = xmlDoc.SelectNodes("//offer");

            foreach (XmlNode offerNode in offerNodes)
            {
                if (offerNode.Attributes["id"] != null)
                {
                    string offerId = offerNode.Attributes["id"].Value;
                    offerIds.Add(offerId);
                }
            }

            return offerIds;
        }

        public async Task<string> GetOfferJsonById(string offerId)
        {
            XmlDocument xmlDoc = await GetAllOfferInfo();
            XmlNode offerNode = xmlDoc.SelectSingleNode($"//offer[@id='{offerId}']");

            string jsonOffer = JsonConvert.SerializeXmlNode(offerNode);
            return jsonOffer;
        }

        private async Task<XmlDocument> GetAllOfferInfo()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_url);
            response.EnsureSuccessStatusCode();

            string xmlString = await response.Content.ReadAsStringAsync();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);

            return xmlDoc;
        }
    }
}
    