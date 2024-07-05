using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesAnalytics.Core.Entities
{
    public class SaleRecord
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Region { get; set; }
    }
}
