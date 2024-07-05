using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.AspNetCore.Mvc;
using SalesAnalytics.Application.DTOs;
using SalesAnalytics.Application.Services;
using SalesAnalytics.Core.Entities;

namespace SalesAnalytics.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaleRecordsController : ControllerBase
    {
        private readonly SaleRecordService _service;
        private readonly ILogger<SaleRecordsController> _logger;

        public SaleRecordsController(SaleRecordService service, ILogger<SaleRecordsController> logger)
        {
            _service = service;
            _logger = logger;   
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSaleRecordById(int id)
        {
           
                _logger.LogInformation($"Getting sale record with ID: {id}");
                var saleRecord = await _service.GetSaleRecordByIdAsync(id);
                if (saleRecord == null)
                {
                    _logger.LogWarning($"Sale record with ID: {id} not found");
                    return NotFound();
                }

                return Ok(saleRecord);
            
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSaleRecords()
        {
            _logger.LogInformation($"Getting List sale record ");
            var saleRecords = await _service.GetAllSaleRecordsAsync();
            _logger.LogInformation($"Getting list success ");
            return Ok(saleRecords);
        }

        [HttpPost]
        public async Task<IActionResult> AddSaleRecord(SaleRecordDto saleRecordDto)
        {
            _logger.LogInformation($"Add Sale Record");
            SaleRecord saleRecord = new SaleRecord()
            {
                ProductName = saleRecordDto.ProductName,
                Amount = saleRecordDto.Amount,
                Date = saleRecordDto.Date,
                Region = saleRecordDto.Region,
            };
            await _service.AddSaleRecordAsync(saleRecord);
            _logger.LogInformation($"Add Sale Record {saleRecord.Id}");

            return CreatedAtAction(nameof(GetSaleRecordById), new { id = saleRecord.Id }, saleRecord);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateSaleRecord(int id , SaleRecord saleRecord)
        {

            _logger.LogInformation($"Record Updated Started");
            if (id != saleRecord.Id)
                return BadRequest();

              
            await _service.UpdateSaleRecordAsync(saleRecord);
            _logger.LogInformation($"Record Updated success");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSaleRecord(int id=1)
        {
            _logger.LogInformation($"Record Deleted Started");
            await _service.DeleteSaleRecordAsync(id);
            _logger.LogInformation($"Record Deleted success");
            return NoContent();
        }

        [HttpGet("totalsales")]
        public async Task<IActionResult> GetTotalSales(DateTime startDate, DateTime endDate)
        {
            _logger.LogInformation($"getting Total Sales Started");
            var totalSales = await _service.GetTotalSalesAsync(startDate, endDate);
            _logger.LogInformation($"getting Total Sales success");
            return Ok(totalSales);
        }
        [HttpGet("GetTotalSalesPrice")]
        public async Task<IActionResult> GetTotalSalesPrice()
        {
            _logger.LogInformation($"getting Total Sales price Started");
            var totalSales = await _service.GetTotalSalesPrice();
            _logger.LogInformation($"getting Total Sales price success");
            return Ok(totalSales);
        }

        [HttpGet("salestrends")]
        public async Task<IActionResult> GetSalesTrends(string interval= "monthly")
        {
            _logger.LogInformation($"getting Get Sales Trends Started");
            var salesTrends = await _service.GetSalesTrendsAsync(interval);
            _logger.LogInformation($"Get Sales Trends success");
            return Ok(salesTrends);
        }


        [HttpGet("topproducts")]
        public async Task<IActionResult> GetTopProducts(DateTime startDate, DateTime endDate, int limit = 10)
        {
            _logger.LogInformation($"getting Get Top Products");
            var topProducts = await _service.GetTopProductsAsync(startDate, endDate, limit);
            _logger.LogInformation($"getting Get Sales Trends success");
            return Ok(topProducts);
        }
        [HttpGet("salesbyregion")]
        public async Task<IActionResult> GetSalesByRegion(DateTime startDate, DateTime endDate)
        {
            _logger.LogInformation($"Get Sales By Region");
            var salesByRegion = await _service.GetSalesByRegionAsync(startDate, endDate);
            _logger.LogInformation($"Get Sales By Region success");
            return Ok(salesByRegion);
        }
        [HttpGet("summaryreport")]
        public async Task<IActionResult> GetSummaryReport(DateTime startDate, DateTime endDate, string interval, int topProductLimit = 10)
        {
            _logger.LogInformation($"Get Summary Report Started");
            var summaryReport = await _service.GetSummaryReportAsync(startDate, endDate, interval, topProductLimit);
            _logger.LogInformation($"Get Summary Report success");
            return Ok(summaryReport);
        }
    }
}
    