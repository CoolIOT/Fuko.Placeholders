# Fuko.PlaceHolders 

This library was inspired by laravel-placeholders library. It is meant for finding and replace placeholders in a template string.

[![](https://img.shields.io/nuget/v/Fuko.PlaceHolders.svg)](https://www.nuget.org/packages/Fuko.PlaceHolders/)

## Installation

You can install this library using the .NET CLI:

```shell
$ dotnet add package Fuko.PlaceHolders --version 1.0.0 
``` 

## Usage

```c#

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

                    // Add the library into IOC
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
                    {"first_name", "John"},
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

```

## Configuration

You can optionally change the configuration of the placeholder markers in your *appsettings.json* file.

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  },
  "PlaceHolders": {
    "Start": "[",
    "End": "]",
    "Thorough": false
  }
}
```