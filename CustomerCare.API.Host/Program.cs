using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace CustomerCare.API.Host
{
    class Program : ServiceBase
    {
        IDisposable webApp;
        static string baseAddress = "http://localhost:20189/";

        static void Main(string[] args)
        {
            using (var service = new Program())
            {
                service.ServiceName = ServiceConfiguration.DisplayName;
                // to run interactive from a console or as a windows service
                if (Environment.UserInteractive)
                {
                    if (args.Contains("/install"))
                    {
                        ManagedInstallerClass.InstallHelper(new string[] { Assembly.GetExecutingAssembly().Location });
                    }
                    else if (args.Contains("/uninstall"))
                    {
                        ManagedInstallerClass.InstallHelper(new string[] { "/u", Assembly.GetExecutingAssembly().Location });
                    }
                    else
                    {
                        Console.Title = service.ServiceName;
                        Console.CancelKeyPress += (sender, e) =>
                        {
                            service.OnStop();
                        };

                        service.OnStart(args);

                        Console.WriteLine();
                        Console.WriteLine($"{service.ServiceName}: {baseAddress}");

                        Console.WriteLine("\r\nPress enter key to stop program\r\n");
                        Console.Read();

                        service.OnStop();
                    }
                }
                else
                {
                    Run(service);
                }
            }
        }

        protected override void OnStart(string[] args)
        {
            webApp = WebApp.Start<Startup>(url: baseAddress);
        }

        protected override void OnStop()
        {
            if (webApp != null)
            {
                webApp.Dispose();
            }
        }
    }

    [RunInstaller(true)]
    public class ManagedInstallation : ServiceInstaller
    {
        public ManagedInstallation()
        {
            var ProcessInstaller = new ServiceProcessInstaller();
            var ServiceInstaller = new ServiceInstaller();

            ProcessInstaller.Account = ServiceConfiguration.AccountType;
            ServiceInstaller.DisplayName = ServiceConfiguration.DisplayName;
            ServiceInstaller.StartType = ServiceConfiguration.StartType;
            ServiceInstaller.Description = ServiceConfiguration.Description;
            ServiceInstaller.ServiceName = ServiceConfiguration.ServiceName;

            Installers.Add(ProcessInstaller);
            Installers.Add(ServiceInstaller);
        }
    }

    class ServiceConfiguration
    {
        public static string DisplayName => "CustomerCare API Host";
        public static string ServiceName => "CustomerCareAPIHost";
        public static string Description => "Hosts Custopmer Care service WebAPI.";
        public static ServiceStartMode StartType => ServiceStartMode.Automatic;
        public static ServiceAccount AccountType => ServiceAccount.LocalSystem;
    }
}
