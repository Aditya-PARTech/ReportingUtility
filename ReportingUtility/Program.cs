using System.Xml.Linq;
using ReportingUtility.Data;

Console.WriteLine("Available calculation types:");
Console.WriteLine("  XML: " + string.Join(", ", CalculatorFactory.GetXmlCalculatorTypes()));
Console.WriteLine("  JSON: " + string.Join(", ", CalculatorFactory.GetJsonCalculatorTypes()));
Console.WriteLine("Enter calculation type:");
string calculationType = Console.ReadLine() ?? string.Empty;

if (CalculatorFactory.IsJsonCalculator(calculationType))
{
    Console.WriteLine("Reading JSON from file...Enter the file path: ");
    string jsonFilePath = Console.ReadLine() ?? string.Empty;
    string jsonString = await File.ReadAllTextAsync(jsonFilePath);

    var calculator = CalculatorFactory.GetJsonCalculator(calculationType);

    if (calculator == null)
    {
        Console.WriteLine("Unknown calculation type.");
    }
    else
    {
        try
        { 
            var result = calculator.Calculate(jsonString);
            // Use reflection to print all CalculationResult properties for extensibility
            Console.WriteLine("Summary:");
            var properties = result.GetType().GetProperties();
            Console.WriteLine($"Property count: {properties.Length}");
            foreach (var prop in properties)
            {
                Console.WriteLine($"{prop.Name}: {prop.GetValue(result)}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to process JSON: " + ex.Message);
        }
    }
}
else if (CalculatorFactory.IsXmlCalculator(calculationType))
{
    Console.WriteLine("Reading XML from file...Enter the file path: ");
    string xmlFilePath = Console.ReadLine() ?? string.Empty;
    string xmlString = await File.ReadAllTextAsync(xmlFilePath);

    var doc = XDocument.Parse(xmlString);
    XNamespace ns = "http://www.brinksoftware.com/webservices/sales/v2";

    var calculator = CalculatorFactory.GetXmlCalculator(calculationType);

    if (calculator == null)
    {
        Console.WriteLine("Unknown calculation type.");
    }
    else
    {
        decimal result = calculator.Calculate((doc, ns));
        Console.WriteLine($"{calculationType} result: {result}");
    }
}
else
{
    Console.WriteLine("Unknown calculation type.");
}

Console.ReadLine();

public record CalculationResult(double LaborCost, double LaborHours, double NetSales, double GrossSales, double Taxes, int OrderCount);