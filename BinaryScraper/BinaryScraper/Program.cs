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
        static void Main(string[] args)
        {
            GetSymbols();
            Console.ReadLine();
        }

        private static List<string> ParseSymbols(string json)
        {
            BinaryModels.RootObjectActiveSymbol sym = JsonConvert.DeserializeObject<BinaryModels.RootObjectActiveSymbol>(json);

            return sym.active_symbols.Where(x => x.market == "forex").Select(x => x.symbol).ToList();
        }

        private static void GetSymbols()
        {
            string request = "{ \"active_symbols\": \"brief\", \"product_type\": \"basic\"}";
            string json = GetData(request);
            symbols = ParseSymbols(json);
        }

        private static void GetValuesFromDates(DateTime startDate, DateTime endDate, string symbol)
        {
            string request = "{ \"active_symbols\": \"brief\", \"product_type\": \"basic\"}";
        }

        private static void Test()
        {
            string request = "{\"ticks\":\"R_100\"}";
            GetStream(request);
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
    }
}
