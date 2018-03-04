using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryScraper.BinaryModels
{
    class ActiveSymbol
    {
        public string display_name { get; set; }
        public int exchange_is_open { get; set; }
        public int is_trading_suspended { get; set; }
        public string market { get; set; }
        public string market_display_name { get; set; }
        public string pip { get; set; }
        public string submarket { get; set; }
        public string submarket_display_name { get; set; }
        public string symbol { get; set; }
        public string symbol_type { get; set; }
        public int? allow_forward_starting { get; set; }
    }
}
