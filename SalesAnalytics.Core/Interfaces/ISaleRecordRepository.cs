using SalesAnalytics.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesAnalytics.Core.Interfaces
{
    public interface ISaleRecordRepository
    {
        Task<SaleRecord> GetSaleRecordByIdAsync(int id);
        Task<IEnumerable<SaleRecord>> GetAllSaleRecordsAsync();
        Task AddSaleRecordAsync(SaleRecord saleRecord);
        void UpdateSaleRecord(SaleRecord saleRecord);
        void DeleteSaleRecord(int id);
        Task<decimal> GetTotalSalesAsync(DateTime startDate, DateTime endDate);
        Task<decimal> GetTotalSalesPriceAsync();
        Task<IDictionary<string, decimal>> GetSalesTrendsAsync(string interval);
        Task<IEnumerable<TopProductDto>> GetTopProductsAsync(DateTime startDate, DateTime endDate, int limit);
        Task<IDictionary<string, decimal>> GetSalesByRegionAsync(DateTime startDate, DateTime endDate);
        Task<SummaryReportDto> GetSummaryReportAsync(DateTime startDate, DateTime endDate, string interval, int topProductLimit);
    }
}
