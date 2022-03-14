using SimpleTrader.FinancialModellingPrepAPI.Services;
using SimpleTrader.Wpf.States.Navigators;
using SimpleTrader.Wpf.ViewModels;
using SimpleTrader.Wpf.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SimpleTrader.Wpf.Commands
{
    public class UpdateCurrentViewModelCommand : ICommand
    {
        private readonly INavigator _navigator;
        private readonly ISimpleTraderViewModelFactory _viewModelFactory;

        public UpdateCurrentViewModelCommand(INavigator navigator, ISimpleTraderViewModelFactory viewModelFactory)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is ViewType p)
            {
                _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(p);
            }
        }
    }
}
