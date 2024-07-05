using Microsoft.EntityFrameworkCore;
using SalesAnalytics.Core.Entities;
using SalesAnalytics.Core.Interfaces;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace SalesAnalytics.Infrastructure.Repositories
{
    public class SaleRecordRepository : ISaleRecordRepository
    {
        private readonly SalesDbContext _context;

        public SaleRecordRepository(SalesDbContext context)
        {
            _context = context;
        }

        public async Task<SaleRecord> GetSaleRecordByIdAsync(int id)
        {
            try
            {
                return await _context.SaleRecords.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<SaleRecord>> GetAllSaleRecordsAsync()
        {
            try
            {
                return await _context.SaleRecords.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        public async Task AddSaleRecordAsync(SaleRecord saleRecord)
        {

            try
            {
                await _context.SaleRecords.AddAsync(saleRecord);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public void UpdateSaleRecord(SaleRecord saleRecord) => _context.SaleRecords.Update(saleRecord);

        public void DeleteSaleRecord(int id)
        {
            try
            {

                var saleRecord = _context.SaleRecords.Find(id);
                if (saleRecord != null)
                {
                    _context.SaleRecords.Remove(saleRecord);
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }


        public async Task<decimal> GetTotalSalesAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                return await _context.SaleRecords
               .Where(s => s.Date >= startDate && s.Date <= endDate)
               .SumAsync(s => s.Amount);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        public async Task<decimal> GetTotalSalesPriceAsync()
        {
            try
            {
                return await _context.SaleRecords.SumAsync(s => s.Amount);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        public async Task<IDictionary<string, decimal>> GetSalesTrendsAsync(string interval)
        {
            try
            {
                var query = _context.SaleRecords.GroupBy(
                    s => interval == "monthly" ? s.Date.Month.ToString() :
                         interval == "weekly" ? CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(s.Date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday).ToString() :
                         s.Date.DayOfWeek.ToString(),
                    (key, g) => new { Key = key, TotalSales = g.Sum(s => s.Amount) });

                return await query.ToDictionaryAsync(g => g.Key, g => g.TotalSales);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public async Task<IEnumerable<TopProductDto>> GetTopProductsAsync(DateTime startDate, DateTime endDate, int limit)
        {
            try
            {
                var query = _context.SaleRecords
                    .Where(s => s.Date >= startDate && s.Date <= endDate)
                    .GroupBy(s => s.ProductName)
                    .Select(g => new TopProductDto
                    {
                        ProductName = g.Key,
                        TotalSales = g.Sum(s => s.Amount)
                    })
                    .OrderByDescending(p => p.TotalSales)
                    .Take(limit);

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        public async Task<IDictionary<string, decimal>> GetSalesByRegionAsync(DateTime startDate, DateTime endDate)
        {

            try
            {
                var query = _context.SaleRecords
                .Where(s => s.Date >= startDate && s.Date <= endDate)
                .GroupBy(s => s.Region)
                .Select(g => new { Region = g.Key, TotalSales = g.Sum(s => s.Amount) });

                return await query.ToDictionaryAsync(g => g.Region, g => g.TotalSales);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public async Task<SummaryReportDto> GetSummaryReportAsync(DateTime startDate, DateTime endDate, string interval, int topProductLimit)
        {
            try
            {
                var totalSalesPrice = await GetTotalSalesPriceAsync();
                var totalSales = await GetTotalSalesAsync(startDate, endDate);
                var salesTrends = await GetSalesTrendsAsync(interval, startDate, endDate);
                var topProducts = await GetTopProductsAsync(startDate, endDate, topProductLimit);
                var salesByRegion = await GetSalesByRegionAsync(startDate, endDate);

                return new SummaryReportDto
                {
                    TotalSales = totalSales,
                    SalesTrends = salesTrends,
                    TopProducts = topProducts,
                    SalesByRegion = salesByRegion,
                    TotalSalesPrice = totalSalesPrice

                };
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        private async Task<IDictionary<string, decimal>> GetSalesTrendsAsync(string interval, DateTime startDate, DateTime endDate)
        {
            var query = _context.SaleRecords
             .Where(s => s.Date >= startDate && s.Date <= endDate)
             .GroupBy(s => interval == "monthly" ? s.Date.Month.ToString() :
                            interval == "weekly" ? CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(s.Date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday).ToString() :
                            s.Date.ToString(),
             (key, g) => new { Key = key, TotalSales = g.Sum(s => s.Amount) });

            return await query.ToDictionaryAsync(g => g.Key, g => g.TotalSales);

        }
    }
}
