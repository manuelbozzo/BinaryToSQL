using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryScraper.BinaryModels
{
    public class History
    {
        public List<string> prices { get; set; }
        public List<string> times { get; set; }
    }

    public class EchoReqHistory
    {
        public int adjust_start_time { get; set; }
        public int count { get; set; }
        public string end { get; set; }
        public int start { get; set; }
        public string style { get; set; }
        public string ticks_history { get; set; }
    }

    public class RootObjectHistory
    {
        public EchoReqHistory echo_req { get; set; }
        public History history { get; set; }
        public string msg_type { get; set; }
    }
}
