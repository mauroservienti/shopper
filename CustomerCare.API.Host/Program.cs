namespace CustomerCare.API.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var service = new ServiceHost())
            {
                service.Run(args);
            }
        }
    }
}
