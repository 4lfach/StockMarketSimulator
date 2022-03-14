using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.Wpf.States.Accounts;
using SimpleTrader.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.Wpf.Commands
{
    public class SellStockCommand : AsyncCommandBase
    {
        private SellViewModel _sellViewModel;
        private ISellStockService _sellStockService;
        private readonly IAccountStore _accStore;

        public SellStockCommand(SellViewModel sellViewModel, ISellStockService sellStockService, IAccountStore accStore)
        {
            _sellViewModel = sellViewModel;
            _sellStockService = sellStockService;
            _accStore = accStore;

            _sellViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName ==nameof(_sellViewModel.CanSellStock))
            {
                OnCanExecuteChanged();
            }
        }
        public override bool CanExecute(object parameter)
        {
            return _sellViewModel.CanSellStock && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _sellViewModel.ErrorMessage = string.Empty;
            _sellViewModel.StatusMessage = string.Empty;

            try
            {
                string symbol = _sellViewModel.Symbol;
                int shares = _sellViewModel.SharesToSell;

                Account acc = await _sellStockService.SellStock(_accStore.CurrentAccount, symbol, shares);

                _accStore.CurrentAccount = acc;

                _sellViewModel.SearchResultSymbol = string.Empty;

                _sellViewModel.StatusMessage = $"Succesfully sold {shares} shares of {symbol}";
            }
            catch (InvalidSymbolException e)
            {
                _sellViewModel.ErrorMessage = "The stock is invalid";
            }
            catch (InsufficientSharesException e)
            {
                _sellViewModel.ErrorMessage = "There are not enough shares to sell";
            }
            catch (Exception e)
            {
                _sellViewModel.ErrorMessage = "Transaction was cancelled";
            }
        }
    }
}
