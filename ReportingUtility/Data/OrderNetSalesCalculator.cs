using System.Xml.Linq;

namespace ExternalAPIUtility.Data
{
    public class OrderNetSalesCalculator
    {
        private readonly XNamespace _namespace;

        public OrderNetSalesCalculator(string xmlNamespace)
        {
            _namespace = xmlNamespace;
        }

        public decimal CalculateTotalNetSales(string xmlString)
        {
            var doc = XDocument.Parse(xmlString);

            return doc
                .Descendants(_namespace + "Order")
                .Sum(order => (decimal?)order.Element(_namespace + "NetSales") ?? 0);
        }
    }
}