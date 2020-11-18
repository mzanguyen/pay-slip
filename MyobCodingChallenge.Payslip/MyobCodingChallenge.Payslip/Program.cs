using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyobCodingChallenge.Payslip.Service.Interface;
using MyobCodingChallenge.Payslip.Service.Models;
using MyobCodingChallenge.Payslip.Service.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyobCodingChallenge.Payslip
{
    public class Program
    {
        public static Task Main(string[] args) =>
            CreateHostBuilder(args).Build().RunAsync();


        public static void PrintPayslip(IPayslipService payslipService)
        {
            while (true)
            {
                Console.WriteLine("Name: ");
                var name = Console.ReadLine();

                Console.WriteLine("Salary:");
                if( !decimal.TryParse(Console.ReadLine(), out decimal salary))
                {
                    Console.WriteLine("Invalid input for salary, please try again");
                    continue;
                }

                Console.WriteLine("\r\n");
                Console.WriteLine(payslipService.GetPayslip(name, salary));
            }
        }

        public static void RunProgram(IServiceCollection services)
        {
            var payslipService = services.BuildServiceProvider().GetService<IPayslipService>();

            PrintPayslip(payslipService);
        }

        public static void Configure(IServiceCollection services, IConfigurationRoot configurationRoot)
        {
            // Configuration from appsettings
            services.Configure<List<TaxBand>>(configurationRoot.GetSection(key: nameof(TaxBand)));

            // Configure services
            services
                .AddLogging()
                .AddSingleton<IIncomeService, IncomeService>()
                .AddSingleton<ITaxService, TaxService>()
                .AddSingleton<IPayslipService, PayslipService>();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {

            return Host.CreateDefaultBuilder(args).ConfigureAppConfiguration((hostingContext, configuration) =>
            {

                configuration.Sources.Clear();

                var services = new ServiceCollection();

                var env = hostingContext.HostingEnvironment;

                configuration
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);

                var configurationRoot = configuration.Build();

                Configure(services, configurationRoot);

                RunProgram(services);
            });
            
        }
        

    }
}
