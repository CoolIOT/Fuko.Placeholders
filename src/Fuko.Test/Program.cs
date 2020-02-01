using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fuko.PlaceHolders;
using Fuko.PlaceHolders.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Fuko.PlaceHolders.Extensions;

namespace Fuko.Test
{
    class Program
    {
        static async  Task Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: false);
                    config.AddEnvironmentVariables();

                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddOptions();

                    services.AddFukoPlaceholders();
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                }).Build();
            
            var template = "Dear [first_name], your loan of KES [amount] has been fully repaid. Thank you for using [app_name]. [app_code]";

            var temp = host.Services.GetService<IPlaceholders>();

            try
            {
                var res = await temp.ParseAsync(template, new Dictionary<string, string>()
                {
                    {"first_name", "Nimrod"},
                    {"amount", "10,000"},
                    {"app_name", "Fuko"}
                });
                Console.WriteLine(res);
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occured  => {e.Message}");
            }


            await host.RunAsync();
        }
    }
}