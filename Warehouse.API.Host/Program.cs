using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.API.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Warehouse.API.Host";
            string baseAddress = "http://localhost:20185/";

            using(WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine();
                Console.WriteLine($"{Console.Title}: {baseAddress}");
                Console.ReadLine();
            }
        }
    }
}
