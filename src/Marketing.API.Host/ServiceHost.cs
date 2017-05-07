using Microsoft.Owin.Hosting;
using System;
using System.Configuration;

namespace Marketing.API.Host
{
    class ServiceHost
    {
        IDisposable webApp;

        public void Start()
        {
            var baseAddress = ConfigurationManager.AppSettings["baseAddress"];

            webApp = WebApp.Start<Startup>(url: baseAddress);
            Console.WriteLine($"Marketing.API.Host listening on {baseAddress}");
        }

        public void Stop()
        {
            if (webApp != null)
            {
                webApp.Dispose();
            }
        }
    }
}
