using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SimpleTrader.Domain.Exceptions
{
    public class InsufficientSharesException : Exception
    {
        public string Symbol { get; }
        public int AccountShares { get; }
        public int RequiredShares { get; }

        public InsufficientSharesException(string symbol, int accountShares, int requiredShares)
        {
            Symbol = symbol;
            AccountShares = accountShares;
            RequiredShares = requiredShares;
        }

        public InsufficientSharesException(string message, string symbol = null, int accountShares = 0, int requiredShares = 0) : base(message)
        {
            Symbol = symbol;
            AccountShares = accountShares;
            RequiredShares = requiredShares;
        }

        public InsufficientSharesException(string message, Exception innerException, string symbol = null, int accountShares = 0, int requiredShares = 0) : base(message, innerException)
        {
            Symbol = symbol;
            AccountShares = accountShares;
            RequiredShares = requiredShares;
        }

    }
}
