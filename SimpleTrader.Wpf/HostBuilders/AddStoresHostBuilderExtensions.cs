using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.Wpf.States.Accounts;
using SimpleTrader.Wpf.States.Assets;
using SimpleTrader.Wpf.States.Authenticators;
using SimpleTrader.Wpf.States.Navigators;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.Wpf.HostBuilders
{
    public static class AddStoresHostBuilderExtensions
    {
        public static IHostBuilder AddStores(this IHostBuilder host)
        {
            return host.ConfigureServices(services =>
            {
                services.AddSingleton<INavigator, Navigator>();
                services.AddSingleton<IAuthenticator, Authenticator>();
                services.AddSingleton<IAccountStore, AccountStore>();
                services.AddSingleton<AssetStore>();
            });
        }
    }
}
