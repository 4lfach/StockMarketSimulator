using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SimpleTrader.Domain.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public string Username { get; set; }
        public UserNotFoundException(string userName)
        {
            Username = userName;
        }

        public UserNotFoundException(string message, string userName) : base(message)
        {
            Username = userName;
        }

        public UserNotFoundException(string message, Exception innerException, string userName) : base(message, innerException)
        {
            Username = userName;
        }

    }
}
