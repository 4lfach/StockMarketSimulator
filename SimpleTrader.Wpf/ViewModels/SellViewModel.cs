using SimpleTrader.Domain.Services;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.Wpf.Commands;
using SimpleTrader.Wpf.States.Accounts;
using SimpleTrader.Wpf.States.Assets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SimpleTrader.Wpf.ViewModels
{
    public class SellViewModel :ViewModelBase, ISearchSymbolViewModel
    {
        public AssetListingViewModel AssetListingViewModel { get; }

        private AssetViewModel _selectedAsset;
        public AssetViewModel SelectedAsset
        {
            get
            {
                return _selectedAsset;
            }
            set
            {
                _selectedAsset = value;
                OnPropertyChanged(nameof(SelectedAsset));
                OnPropertyChanged(nameof(Symbol));
                OnPropertyChanged(nameof(CanSearchSymbol));
            }
        }

        public string Symbol => SelectedAsset?.Symbol;

        private string _searchResultSymbol = string.Empty;
        public string SearchResultSymbol
        {
            get
            {
                return _searchResultSymbol;
            }
            set
            {
                _searchResultSymbol = value;
                OnPropertyChanged(nameof(SearchResultSymbol));
            }
        }
        private double _stockPrice;

        public double StockPrice
        {
            get { return _stockPrice; }
            set
            {
                _stockPrice = value;
                OnPropertyChanged(nameof(StockPrice));
            }
        }

        private int _sharesToSell;
        public int SharesToSell
        {
            get
            {
                return _sharesToSell;
            }
            set
            {
                _sharesToSell = value;
                OnPropertyChanged(nameof(SharesToSell));
                OnPropertyChanged(nameof(TotalValue));
                OnPropertyChanged(nameof(CanSellStock));
            }
        }

        public double TotalValue => SharesToSell * StockPrice;

        public MessageViewModel ErrorMessageViewModel { get; }
        public string ErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;
        }
        public MessageViewModel StatusMessageViewModel { get; }
        public string StatusMessage
        {
            set => StatusMessageViewModel.Message = value;
        }

        public ICommand SearchSymbolCommand { get; }
        public ICommand SellStockCommand { get; }

        public bool CanSearchSymbol => !string.IsNullOrEmpty(Symbol);

        public bool CanSellStock => SharesToSell > 0;

        public SellViewModel(AssetStore assetStore, IStockPriceService stockPriceSerive, ISellStockService sellStockService, IAccountStore accStore)
        {
            AssetListingViewModel = new AssetListingViewModel(assetStore);

            SearchSymbolCommand = new SearchSymbolCommand(this, stockPriceSerive);
            SellStockCommand = new SellStockCommand(this, sellStockService, accStore);

            ErrorMessageViewModel = new MessageViewModel();
            StatusMessageViewModel = new MessageViewModel();
        }

        public override void Dispose()
        {
            AssetListingViewModel.Dispose();
            ErrorMessageViewModel.Dispose();
            StatusMessageViewModel.Dispose();

            base.Dispose();
        }
    }
}
