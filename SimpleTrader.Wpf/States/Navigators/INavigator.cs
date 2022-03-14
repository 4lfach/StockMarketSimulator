using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using SimpleTrader.Wpf.ViewModels;

namespace SimpleTrader.Wpf.States.Navigators
{
    public enum ViewType
    {
        Home, Portfolio, Buy, Sell, Login, Register
    }
    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }
        event Action StateChanged;
    }
}
