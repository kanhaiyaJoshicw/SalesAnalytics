using SalesAnalytics.Core.Entities;

public class SummaryReportDto
{
    public decimal TotalSales { get; set; }
    public IDictionary<string, decimal> SalesTrends { get; set; }
    public IEnumerable<TopProductDto> TopProducts { get; set; }
    public IDictionary<string, decimal> SalesByRegion { get; set; }
    public decimal TotalSalesPrice { get; set; }
}