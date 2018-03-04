using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BinaryScraper
{
    class Program
    {
        static List<string> symbols;
        static DateTime startDate = new DateTime(2018, 1, 1);
        static DateTime endDate = DateTime.Now;
        static void Main(string[] args)
        {
            GetSymbols();
            foreach (string symbol in symbols)
            {
                GetValuesFromDates(startDate, endDate, symbol);
            }
            Console.ReadLine();
        }

        private static List<string> ParseSymbols(string json)
        {
            BinaryModels.RootObjectActiveSymbol sym = JsonConvert.DeserializeObject<BinaryModels.RootObjectActiveSymbol>(json);

            return sym.active_symbols.Where(x => x.market == "forex").Select(x => x.symbol).ToList();
        }

        private static List<BinaryModels.Ticks> ParseHistory(string json)
        {
            BinaryModels.RootObjectHistory sym = JsonConvert.DeserializeObject<BinaryModels.RootObjectHistory>(json);

            List<BinaryModels.Ticks> result = new List<BinaryModels.Ticks>();
            for (int i = 0; i < sym.history.times.Count; i++)
            {
                result.Add(new BinaryModels.Ticks { epoch = sym.history.times[i], value = sym.history.prices[i], quote = sym.echo_req.ticks_history });
            }
            return result;
        }

        private static void GetSymbols()
        {
            string request = "{ \"active_symbols\": \"brief\", \"product_type\": \"basic\"}";
            string json = GetData(request);
            symbols = ParseSymbols(json);
        }

        private static List<BinaryModels.Ticks> GetValuesFromDates(DateTime startDate, DateTime endDate, string symbol)
        {
            long startEpoch = calculateSeconds(startDate);
            long endEpoch = calculateSeconds(endDate);
            string maxEpochInPartial = startEpoch.ToString();
            List<BinaryModels.Ticks> ticks = new List<BinaryModels.Ticks>();

            do
            {
                endEpoch = startEpoch + 5000 > calculateSeconds(endDate) ? calculateSeconds(endDate) : startEpoch + 5000;
                string request = "{ \"ticks_history\": \"" + symbol + "\", \"end\": \"" + endEpoch.ToString() + "\", \"start\": \"" + startEpoch.ToString() + "\", \"style\": \"ticks\" }";
                string json = GetData(request);

                ticks.AddRange(ParseHistory(json));
                Console.WriteLine("ticks count: " + ticks.Count +" / " + FromUnixTime(Convert.ToInt64(ticks.Max(x => x.epoch))).ToString() );

                startEpoch = endEpoch + 1;

            } while (endEpoch < calculateSeconds(endDate));
            return ticks;
        }

        private static void GetStream(string request)
        {
            var bws = new BinaryConnection();
            bws.Connect().Wait();

            bws.SendRequest(request).Wait();
            bws.StartListenStream();

        }

        private static string GetData(string request)
        {
            var bws = new BinaryConnection();
            bws.Connect().Wait();

            bws.SendRequest(request).Wait();
            var response = bws.StartListen();

            return response.Result;
        }

        public static long calculateSeconds(DateTime date)

        {
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);
            TimeSpan result = date.Subtract(dt);
            long seconds = Convert.ToInt32(result.TotalSeconds);

            return seconds;

        }

        public static DateTime FromUnixTime(long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime);
        }
    }
}
