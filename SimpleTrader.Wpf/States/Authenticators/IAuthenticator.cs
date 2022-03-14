using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.AuthenticationServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SimpleTrader.Domain.Exceptions;

namespace SimpleTrader.Wpf.States.Authenticators
{
    public interface IAuthenticator
    {
        Account CurrentAccount { get; }
        bool IsLoggedIn { get; }

        Task<RegistrationResult> Register(string email,string username, string password, string confirmPassword);

        /// <summary>
        /// Login into the application.
        /// </summary>
        /// <param name="username">The account's username</param>
        /// <param name="password">The account's password</param>
        /// <exception cref = "UserNotFoundException" > Thrown if the user does not exist.</exception>
        /// <exception cref="InvalidPasswordException">Thrown if the password is invalid.</exception>
        /// <exception cref="Exception">Thrown if the login fails.</exception>
        Task Login( string username, string password);
        void LogOut();

        event Action StateChanged;
    }
}
