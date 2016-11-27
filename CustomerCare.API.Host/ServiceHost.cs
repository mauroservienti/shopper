using Microsoft.Owin.Hosting;
using System;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;

namespace CustomerCare.API.Host
{
    class ServiceHost : ServiceBase
    {
        IDisposable webApp;
        string baseAddress = "http://localhost:20189/";
        
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

        internal void Run(string[] args)
        {
            this.ServiceName = ServiceConfiguration.ServiceName;
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
                    Console.Title = ServiceConfiguration.DisplayName;
                    Console.CancelKeyPress += (sender, e) =>
                    {
                        this.OnStop();
                    };

                    this.OnStart(args);

                    Console.WriteLine();
                    Console.WriteLine($"{this.ServiceName}: {baseAddress}");

                    Console.WriteLine("\r\nPress enter key to stop program\r\n");
                    Console.Read();

                    this.OnStop();
                }
            }
            else
            {
                Run(this);
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
