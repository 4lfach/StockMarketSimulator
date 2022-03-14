using SimpleTrader.Wpf.States.Assets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SimpleTrader.Wpf.ViewModels
{
    public class PortfolioViewModel :ViewModelBase
    {
        public AssetListingViewModel AssetListingViewModel { get; }

        public PortfolioViewModel(AssetStore assetStore)
        {
            AssetListingViewModel = new AssetListingViewModel(assetStore);
        }

        public override void Dispose()
        {
            AssetListingViewModel.Dispose();

            base.Dispose();
        }
    }
}
