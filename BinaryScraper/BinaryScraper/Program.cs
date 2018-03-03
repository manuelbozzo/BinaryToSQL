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


            string data = "{\"ticks\":\"R_100\"}";

            var bws = new BinaryConnection();
            bws.Connect().Wait();

            bws.SendRequest(data).Wait();
            bws.StartListen();

            Console.ReadLine();
        }
    }
}
