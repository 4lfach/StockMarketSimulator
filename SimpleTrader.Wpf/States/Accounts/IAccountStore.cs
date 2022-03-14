using SimpleTrader.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.Wpf.States.Accounts
{
    public interface IAccountStore
    {
        public Account CurrentAccount { get; set; }

        event Action StateChanged;
    }
}
