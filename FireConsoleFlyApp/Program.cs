using JfFireflyWebApp.Shared;
using JfFireflyWebApp.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace FireConsoleFlyApp
{
    class Program
    {
        private static IConfiguration _configuration => new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false)
                .Build();

        private static IServiceProvider _serviceProvider;

        private static void BuildServiceProvider()
        {
            var services = new ServiceCollection();

            var configSection = _configuration.GetSection("AppSettings");
            var settings = configSection.Get<AppSettings>();
            services.AddSingleton(settings);

            services.AddTransient<IFireTaxService, FireTaxService>();

            _serviceProvider = services.BuildServiceProvider();
        }

        static async Task Main(string[] args)
        {
            BuildServiceProvider();

            await TestFireflyConnectionAsync();

            Console.ReadLine();
        }

        static async Task TestFireflyConnectionAsync()
        {
            Console.WriteLine("Start...");
            var s = _serviceProvider.GetService<IFireTaxService>();

            var r = await s.GetSystemInfoAsync();

            Console.WriteLine($"You have version {r.Data.Version} installed");
        }
    }
}
