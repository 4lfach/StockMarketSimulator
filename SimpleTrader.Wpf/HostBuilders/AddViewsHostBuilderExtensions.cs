using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.Wpf.HostBuilders
{
    public static class AddViewsHostBuilderExtensions
    {
        public static IHostBuilder AddViews(this IHostBuilder host)
        {
            host.ConfigureServices((context, services) => services.AddSingleton(s => new MainWindow(s.GetRequiredService<MainViewModel>())));

            return host;
        }
    }
}
