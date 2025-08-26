using System.Xml.Linq;

namespace ReportingUtility.Data
{
    public class OrderCountCalculator : IXmlOrderCalculator
    {
        public decimal Calculate((XDocument doc, XNamespace ns) input)
        {
            var (doc, ns) = input;
            return doc.Descendants(ns + "Order").Count();
        }
    }
}