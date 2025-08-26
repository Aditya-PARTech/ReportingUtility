using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace ReportingUtility.Data
{
    public class HourlyData
    {
        public int Id { get; set; }
        public int Hour { get; set; }
        public double Taxes { get; set; }
        public double NetSales { get; set; }
        public double LaborCost { get; set; }
        public double GrossSales { get; set; }
        public int GuestCount { get; set; }
        public int OrderCount { get; set; }
        public int LaborMinutes { get; set; }
    }

    public class JsonLaborCostCalculator : IJsonLaborCalculator
    {
        public CalculationResult Calculate(string json)
        {
            List<HourlyData>? data = JsonSerializer.Deserialize<List<HourlyData>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (data == null)
                throw new InvalidOperationException("Failed to parse JSON or no data found.");

            double totalLaborCost = data.Sum(d => d.LaborCost);
            double totalLaborHours = data.Sum(d => d.LaborMinutes) / 60.0;
            double totalNetSales = data.Sum(d => d.NetSales);
            double totalGrossSales = data.Sum(d => d.GrossSales);
            double totalTaxes = data.Sum(d => d.Taxes);
            int totalOrderCount = data.Sum(d => d.OrderCount);

            return new CalculationResult(
                totalLaborCost,
                totalLaborHours,
                totalNetSales,
                totalGrossSales,
                totalTaxes,
                totalOrderCount
            );
        }
    }
}