using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryScraper
{
    class Program
    {
        static void Main(string[] args)
        {

            GetSymbols();
            Console.ReadLine();
        }

        private static void GetSymbols()
        {
            string request = "{ \"active_symbols\": \"brief\", \"product_type\": \"basic\"}";
            GetData(request);
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
