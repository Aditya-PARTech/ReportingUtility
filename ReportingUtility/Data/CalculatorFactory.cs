using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ReportingUtility.Data
{
    public static class CalculatorFactory
    {
        private static readonly Dictionary<string, Func<IXmlOrderCalculator>> XmlCalculators =
            new(StringComparer.OrdinalIgnoreCase)
            {
                { "netsales", () => new NetSalesCalculator() },
                { "ordercount", () => new OrderCountCalculator() }
                // Add more XML calculators here
            };

        private static readonly Dictionary<string, Func<IJsonLaborCalculator>> JsonCalculators =
            new(StringComparer.OrdinalIgnoreCase)
            {
                { "jsonlaborcost", () => new JsonLaborCostCalculator() }
                // Add more JSON calculators here
            };

        public static IEnumerable<string> GetXmlCalculatorTypes() => XmlCalculators.Keys;
        public static IEnumerable<string> GetJsonCalculatorTypes() => JsonCalculators.Keys;

        public static bool IsXmlCalculator(string calculationType) =>
            !string.IsNullOrWhiteSpace(calculationType) && XmlCalculators.ContainsKey(calculationType);

        public static bool IsJsonCalculator(string calculationType) =>
            !string.IsNullOrWhiteSpace(calculationType) && JsonCalculators.ContainsKey(calculationType);

        public static IXmlOrderCalculator? GetXmlCalculator(string calculationType)
        {
            if (XmlCalculators.TryGetValue(calculationType, out var factory))
                return factory();
            return null;
        }

        public static IJsonLaborCalculator? GetJsonCalculator(string calculationType)
        {
            if (JsonCalculators.TryGetValue(calculationType, out var factory))
                return factory();
            return null;
        }
    }
}