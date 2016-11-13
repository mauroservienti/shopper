using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.API.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Shipping.API.Host";
            string baseAddress = "http://localhost:20186/";

            using(WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine();
                Console.WriteLine($"{Console.Title}: {baseAddress}");
                Console.ReadLine();
            }
        }
    }
}
