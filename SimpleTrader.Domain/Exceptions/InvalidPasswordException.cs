using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SimpleTrader.Domain.Exceptions
{
    public class InvalidPasswordException : Exception
    {
        public string Password { get; set; }
        public string Username { get; set; }
        public InvalidPasswordException(string password, string username)
        {
            Password = password;
            Username = username;
        }

        public InvalidPasswordException(string message, string password, string username) : base(message)
        {
            Password = password;
            Username = username;
        }

        public InvalidPasswordException(string message, Exception innerException, string password, string username) : base(message, innerException)
        {
            Password = password;
            Username = username;
        }

    }
}
