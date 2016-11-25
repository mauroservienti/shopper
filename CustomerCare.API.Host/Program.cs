using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace CustomerCare.API.Host
{
    class Program : ServiceBase
    {
        IDisposable webApp;

        static void Main(string[] args)
        {
            using (var service = new Program())
            {
                service.ServiceName = "CustomerCare.API.Host";
                // to run interactive from a console or as a windows service
                if (Environment.UserInteractive)
                {
                    Console.Title = service.ServiceName;
                    Console.CancelKeyPress += (sender, e) =>
                    {
                        service.OnStop();
                    };

                    service.OnStart(args);

                    Console.WriteLine("\r\nPress enter key to stop program\r\n");
                    Console.Read();

                    service.OnStop();

                    return;
                }
                Run(service);
            }
        }

        protected override void OnStart(string[] args)
        {
            string baseAddress = "http://localhost:20189/";

            webApp = WebApp.Start<Startup>(url: baseAddress);

            Console.WriteLine();
            Console.WriteLine($"{Console.Title}: {baseAddress}");
        }

        protected override void OnStop()
        {
            if (webApp != null)
            {
                webApp.Dispose();
            }
        }
    }
}
