namespace SalesAnalytics.Application.DTOs
{
    public class SaleRecordDto
    {
        public string ProductName { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Region { get; set; }
    }
}
