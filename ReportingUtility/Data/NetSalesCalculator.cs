using System.Xml.Linq;

namespace ReportingUtility.Data
{
    public class NetSalesCalculator : IXmlOrderCalculator
    // Removed duplicate NetSalesCalculator class to resolve CS0101 error.
    // This file should be deleted or renamed if another definition exists elsewhere.
    {
        public decimal Calculate((XDocument doc, XNamespace ns) input)
        {
            var (doc, ns) = input;
            return doc
                .Descendants(ns + "Order")
                .Sum(order => (decimal?)order.Element(ns + "NetSales") ?? 0);
        }
    }
}