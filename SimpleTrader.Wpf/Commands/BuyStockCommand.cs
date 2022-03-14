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
using System.Windows;
using System.Windows.Input;

namespace SimpleTrader.Wpf.Commands
{
    public class BuyStockCommand : AsyncCommandBase
    {
        private BuyViewModel _buyViewModel;
        private IBuyStockService _buyStockService;
        private readonly IAccountStore _accStore;

        public BuyStockCommand(BuyViewModel buyViewModel, IBuyStockService buyStockService, IAccountStore accStore)
        {
            _buyViewModel = buyViewModel;
            _buyStockService = buyStockService;
            _accStore = accStore;

            _buyViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_buyViewModel.CanBuyStock))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return _buyViewModel.CanBuyStock && base.CanExecute(parameter);
        }

        public async override Task ExecuteAsync(object parameter)
        {
            _buyViewModel.StatusMessage = String.Empty;
            _buyViewModel.ErrorMessage = String.Empty; 

            try
            {
                string symbol = _buyViewModel.Symbol;
                int shares = _buyViewModel.SharesToBuy;

                Account acc = await _buyStockService.BuyStock(_accStore.CurrentAccount, _buyViewModel.Symbol.ToUpper(), _buyViewModel.SharesToBuy);

                _accStore.CurrentAccount = acc;

                _buyViewModel.StatusMessage = $"Succesfully purchased {shares} shares of {symbol}";
            }
            catch (InsufficientFundsException)
            {
                _buyViewModel.ErrorMessage = "Account has insufficient balance. Please transfer more money into your account.";
            }
            catch (InvalidSymbolException) 
            {
                _buyViewModel.ErrorMessage = "Symbol does not exist";
            }
            catch (Exception e)
            {
                _buyViewModel.ErrorMessage = "Transaction Failed";
            }
        }
    }
}
