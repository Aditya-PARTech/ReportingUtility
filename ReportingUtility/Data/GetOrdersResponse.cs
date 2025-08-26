using ServiceReference1;
using System.Xml.Serialization;

namespace ReportingUtility.Data
{
    [XmlRoot(ElementName = "GetOrdersResponse", Namespace = "http://www.brinksoftware.com/webservices/sales/v2")]
    public class GetOrdersDto : GetOrdersResponse
    {

    }
}
