
namespace Notissimus.Models
{
    public class Offer
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Bid { get; set; }
        public bool Available { get; set; }
        public string Url { get; set; }
        public decimal Price { get; set; }
    }
}