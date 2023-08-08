

using Newtonsoft.Json;
using Notissimus.Models;
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

        public async Task<List<Offer>> GetOffers()
        {
            List<Offer> offers = new List<Offer>();

            XmlDocument xmlDoc = await GetAllOfferInfo();

            XmlNodeList offerNodes = xmlDoc.SelectNodes("//offer");

            foreach (XmlNode offerNode in offerNodes)
            {
                Offer offerData = new Offer
                {
                    Id = offerNode.Attributes["id"].Value,
                    Type = offerNode.Attributes["type"].Value,
                    Bid = offerNode.Attributes["bid"].Value,
                    Available = Convert.ToBoolean(offerNode.Attributes["available"].Value),
                    Url = offerNode.SelectSingleNode("url").InnerText,
                    Price = Convert.ToDecimal(offerNode.SelectSingleNode("price").InnerText)
                };

                offers.Add(offerData);
            }

            return offers;
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

        //осталось от предыдущей версии
/*        public async Task<string> GetOfferJsonById(string offerId)
        {
            XmlDocument xmlDoc = await GetAllOfferInfo();
            XmlNode offerNode = xmlDoc.SelectSingleNode($"//offer[@id='{offerId}']");

            string jsonOffer = JsonConvert.SerializeXmlNode(offerNode);
            return jsonOffer;
        }*/
    }
}
    