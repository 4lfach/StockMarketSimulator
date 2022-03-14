using SimpleTrader.Wpf.States.Assets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SimpleTrader.Wpf.ViewModels
{
    public class AssetListingViewModel :ViewModelBase
    {
        private readonly AssetStore _assetStore;
        private readonly ObservableCollection<AssetViewModel> _assets;
        private readonly Func<IEnumerable<AssetViewModel>, IEnumerable<AssetViewModel>> _filterAssets;

        public IEnumerable<AssetViewModel> Assets => _assets;

        public AssetListingViewModel(AssetStore assetStore) : this(assetStore, assets => assets)
        {
        }

        public AssetListingViewModel(AssetStore assetStore, Func<IEnumerable<AssetViewModel>, IEnumerable<AssetViewModel>> filterAssets)
        {
            _assetStore = assetStore;
            _filterAssets= filterAssets;
            _assets = new ObservableCollection<AssetViewModel>();

            _assetStore.StateChanged += AssetStore_OnStateChanged;

            ResetAssets();
        }

        private void AssetStore_OnStateChanged()
        {
            ResetAssets();
        }

        private void ResetAssets()
        {
            IEnumerable<AssetViewModel> assetViewModels = _assetStore.AssetTransactions
                .GroupBy(t => t.Asset.Symbol)
                .Select(g => new AssetViewModel(g.Key, g.Sum(a => a.IsPurchase ? a.SharesAmount : -a.SharesAmount)))
                .Where(a => a.Shares > 0)
                .OrderByDescending(a => a.Shares);

            DisposeAssets();
            _assets.Clear();

            foreach (AssetViewModel assetViewModel in assetViewModels)
            {
                _assets.Add(assetViewModel);
            }
        }

        private void DisposeAssets()
        {
            foreach (AssetViewModel item in _assets)
            {
                item.Dispose();
            }
        }

        public override void Dispose()
        {
            _assetStore.StateChanged -= AssetStore_OnStateChanged;
            DisposeAssets();

            base.Dispose(); 
        }
    }
}
