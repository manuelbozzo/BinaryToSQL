using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryScraper
{
    static class SQLInterfase
    {
        static ChipreDataSet.RatesDataTable fullMap = new ChipreDataSet.RatesDataTable();
        static ChipreDataSetTableAdapters.RatesTableAdapter RTA = new ChipreDataSetTableAdapters.RatesTableAdapter();

        public static void InsertSingleRate(BinaryModels.Ticks tick)
        {
            RTA.InsertRates(tick.quote, Convert.ToDouble(tick.value), FromUnixTime(Convert.ToInt64(tick.epoch)));
        }

        public static void DeleteAll()
        {
            RTA.DeleteAllQuery();
        }

        public static DateTime FromUnixTime(long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime);
        }
    }
}
