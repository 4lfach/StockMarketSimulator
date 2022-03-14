using SimpleTrader.Wpf.States.Navigators;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.Wpf.ViewModels.Factories
{
    public interface ISimpleTraderViewModelFactory
    {
        ViewModelBase CreateViewModel(ViewType viewType);
    }
}
