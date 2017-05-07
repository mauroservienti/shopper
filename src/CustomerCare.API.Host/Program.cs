using Topshelf;

namespace CustomerCare.API.Host
{
    class Program
    {
        public static void Main()
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

                x.SetDescription("Services UI COmposition sample: CustomerCare API Host");
                x.SetDisplayName("CustomerCare API Host");
                x.SetServiceName("CustomerCareAPIHost");
            });
        }
    }
}
