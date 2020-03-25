using System;
using System.Collections.Generic;
using System.Text;

namespace StockManagement.Repository.Model
{
    public class StockDetails
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }

        public decimal CurrentStockPrice { get; set; }

        public decimal DeltaInPrice { get; set; }

        public decimal DeltaInPercentage { get; set; }
    }
}
