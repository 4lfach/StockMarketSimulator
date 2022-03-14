using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.Wpf.ViewModels
{
    public class AssetViewModel : ViewModelBase
    {
        public AssetViewModel(string symbol, int shares)
        {
            Symbol = symbol;
            Shares = shares;
        }

        public string Symbol { get; set; }
        public int Shares { get; }
    }
}
