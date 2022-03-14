using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.FinancialModellingPrepAPI;
using SimpleTrader.FinancialModellingPrepAPI.Models;
using System;

namespace SimpleTrader.Wpf.HostBuilders
{
    public static class AddFinanceApiHostBuilder
    {
        public static IHostBuilder AddFinanceAPI(this IHostBuilder host)
        {
            host.ConfigureServices((context, services) =>
            {
                string apiKey = context.Configuration.GetValue<string>("FINANCE_API_KEY");

                services.AddSingleton(new FinancialModellingPrepAPIKey(apiKey));

                services.AddHttpClient<FinancialModellingPrepHttpClient>(c =>
                {
                    c.BaseAddress = new Uri("https://financialmodelingprep.com/api/v3/");
                });

            });

            return host;
        }
    }
}
