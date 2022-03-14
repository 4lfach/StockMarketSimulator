using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.Domain.Services.AuthenticationServices;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.EntityFramework;
using SimpleTrader.EntityFramework.Services;
using SimpleTrader.FinancialModellingPrepAPI;
using SimpleTrader.FinancialModellingPrepAPI.Services;
using SimpleTrader.Wpf.HostBuilders;
using SimpleTrader.Wpf.States.Accounts;
using SimpleTrader.Wpf.States.Assets;
using SimpleTrader.Wpf.States.Authenticators;
using SimpleTrader.Wpf.States.Navigators;
using SimpleTrader.Wpf.ViewModels;
using SimpleTrader.Wpf.ViewModels.Factories;
using System;
using System.Configuration;
using System.Windows;

namespace SimpleTrader.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = CreateHostBuilder().Build();
        }

        public static IHostBuilder CreateHostBuilder(string [] args = null)
        {
            return Host.CreateDefaultBuilder(args)
                .AddConfiguration()
                .AddDbContext()
                .AddServices()
                .AddViewModels()
                .AddStores()
                .AddFinanceAPI()
                .AddViews();
                
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            SimpleTraderDbContextFactory contextFactory = _host.Services.GetRequiredService<SimpleTraderDbContextFactory>();
            using ( SimpleTraderDbContext context = contextFactory.CreateDbContext())
            {
                context.Database.Migrate();
            }

            Window window = _host.Services.GetRequiredService<MainWindow>();
            window.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();

            base.OnExit(e);
        }
    }
}
