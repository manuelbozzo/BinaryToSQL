using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryScraper.BinaryModels
{
    class RootObject
    {
        public List<ActiveSymbol> active_symbols { get; set; }
        public EchoReq echo_req { get; set; }
        public string msg_type { get; set; }
    }
}
