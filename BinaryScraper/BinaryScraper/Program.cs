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
            Console.WriteLine("Hello World!");

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            Test();
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
            bws.StartListen();

            Console.ReadLine();
        }

        private static void GetData(string request)
        {
            var bws = new BinaryConnection();
            bws.Connect().Wait();

            bws.SendRequest(request).Wait();
            bws.StartListen();

            Console.ReadLine();
        }
    }
}
