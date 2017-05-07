using Microsoft.Owin.Hosting;
using System;
using Topshelf;

namespace Marketing.API.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<ServiceHost>(s =>
                {
                    s.ConstructUsing(name => new ServiceHost());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalService();
                x.StartAutomatically();

                x.SetDescription("Services UI Composition sample: Marketing API Host");
                x.SetDisplayName("Marketing API Host");
                x.SetServiceName("MarketingAPIHost");
            });
        }
    }
}
