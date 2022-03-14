using SimpleTrader.Wpf.States.Assets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SimpleTrader.Wpf.ViewModels
{
    public class AssetSummaryViewModel : ViewModelBase
    {
        private readonly AssetStore _assetStore;

        public double AccountBalance => _assetStore.AccountBalance;
        public AssetListingViewModel AssetListingViewModel { get; }

        public AssetSummaryViewModel(AssetStore assetStore)
        {
            _assetStore = assetStore;

            AssetListingViewModel = new AssetListingViewModel(assetStore, assets => assets.Take(3));

            _assetStore.StateChanged += AssetStore_OnStateChanged;
        }

        private void AssetStore_OnStateChanged()
        {
            OnPropertyChanged(nameof(AccountBalance));
        }

        public override void Dispose()
        {
            _assetStore.StateChanged -= AssetStore_OnStateChanged;
            AssetListingViewModel.Dispose();

            base.Dispose();
        }

        ~AssetSummaryViewModel()
        {

        }
    }
}
