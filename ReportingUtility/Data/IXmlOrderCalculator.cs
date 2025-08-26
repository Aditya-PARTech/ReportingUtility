using System.Xml.Linq;

namespace ReportingUtility.Data
{
    public interface IXmlOrderCalculator : ICalculator<(XDocument doc, XNamespace ns), decimal>
    {
    }
}