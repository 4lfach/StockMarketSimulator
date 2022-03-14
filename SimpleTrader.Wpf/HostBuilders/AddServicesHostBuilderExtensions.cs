using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.AuthenticationServices;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.Domain.Services;
using SimpleTrader.EntityFramework.Services;
using SimpleTrader.FinancialModellingPrepAPI.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNet.Identity;

namespace SimpleTrader.Wpf.HostBuilders
{
    public static class AddServicesHostBuilderExtensions
    {
        public static IHostBuilder AddServices(this IHostBuilder host)
        {
            host.ConfigureServices((context, services) =>
            {
                services.AddSingleton<IDataService<Account>, AccountDataService>();
                services.AddSingleton<IAccountService, AccountDataService>();
                services.AddSingleton<IStockPriceService, StockPriceService>();
                services.AddSingleton<IBuyStockService, BuyStockService>();
                services.AddSingleton<IMajorIndexService, MajorIndexService>();
                services.AddSingleton<IAuthenticationService, AuthenticationService>();
                services.AddSingleton<ISellStockService, SellStockService>();
                services.AddSingleton<IPasswordHasher, PasswordHasher>();
            });
            return host;
        }
    }
}
