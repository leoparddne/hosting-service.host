using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;


namespace HostingService
{
    public class Program
    {
        public static void Main(string[] args)
        {

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var hostBuilder = Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    //services.AddHostedService<EAPCommandAgentJob>();

                    services.AddHostedService<HostingExeJob>();
                });

            if (OperatingSystem.IsLinux())
            {
                hostBuilder.UseSystemd();
            }

            if (OperatingSystem.IsWindows())
            {
                hostBuilder.UseWindowsService();
            }
            return hostBuilder;
        }
    }
}
