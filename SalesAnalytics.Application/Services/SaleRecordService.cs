using Microsoft.EntityFrameworkCore;
using SalesAnalytics.Core;
using SalesAnalytics.Core.Entities;
using SalesAnalytics.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesAnalytics.Application.Services
{
    public class SaleRecordService
    {
        private readonly ISaleRecordRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public SaleRecordService(ISaleRecordRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SaleRecord> GetSaleRecordByIdAsync(int id)
        {
         return   await _repository.GetSaleRecordByIdAsync(id);
        }
      

        public async Task<IEnumerable<SaleRecord>> GetAllSaleRecordsAsync() => await _repository.GetAllSaleRecordsAsync();

        public async Task AddSaleRecordAsync(SaleRecord saleRecord)
        {
            await _repository.AddSaleRecordAsync(saleRecord);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateSaleRecordAsync(SaleRecord saleRecord)
        {
            _repository.UpdateSaleRecord(saleRecord);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteSaleRecordAsync(int id)
        {
            _repository.DeleteSaleRecord(id);
            await _unitOfWork.CompleteAsync();
        }
        public async Task<decimal> GetTotalSalesAsync(DateTime startDate, DateTime endDate)
        {
          return await _repository.GetTotalSalesAsync(startDate, endDate);
        }

        public async Task<decimal> GetTotalSalesPrice()
        {
            return await _repository.GetTotalSalesPriceAsync();
        }

        public async Task<IDictionary<string, decimal>> GetSalesTrendsAsync(string interval)
        {
            return await _repository.GetSalesTrendsAsync(interval);
        }
       public async Task<IEnumerable<TopProductDto>> GetTopProductsAsync(DateTime startDate, DateTime endDate, int limit)
        {
            return await _repository.GetTopProductsAsync(startDate, endDate, limit);
        }
      public  async Task<IDictionary<string, decimal>> GetSalesByRegionAsync(DateTime startDate, DateTime endDate)
        {
            return await _repository.GetSalesByRegionAsync(startDate, endDate);
        }

        public async Task<SummaryReportDto> GetSummaryReportAsync(DateTime startDate, DateTime endDate, string interval, int topProductLimit)
        {
           return await _repository.GetSummaryReportAsync(startDate, endDate, interval, topProductLimit);
        }
    }
}
