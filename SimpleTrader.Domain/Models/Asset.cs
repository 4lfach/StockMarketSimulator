using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.Domain.Models
{
    public class Asset
    {
        private string _symbol;

        public string Symbol
        {
            get { return _symbol; }
            set { _symbol = value?.ToUpper(); }
        }


        public double PricePerShare { get; set; }
    }
}
