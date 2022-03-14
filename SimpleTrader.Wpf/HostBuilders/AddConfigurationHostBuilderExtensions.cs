using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.Wpf.HostBuilders
{
    public static class AddConfigurationHostBuilderExtensions
    {
        public static IHostBuilder AddConfiguration(this IHostBuilder host)
        {
            host.ConfigureAppConfiguration(app =>
            {
                app.AddJsonFile("appsettings.json");
                app.AddEnvironmentVariables();
            });

            return host;
        }
    } 
}
