using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.AuthenticationServices;
using SimpleTrader.Wpf.States.Accounts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.Wpf.States.Authenticators
{
    public class Authenticator : IAuthenticator
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IAccountStore _accStore;

        public Authenticator(IAuthenticationService authenticationService, IAccountStore accStore)
        {
            _authenticationService = authenticationService;
            _accStore = accStore;
        }

        public Account CurrentAccount
        {
            get
            {
                return _accStore.CurrentAccount;
            }
            private set
            {
                _accStore.CurrentAccount = value;
                StateChanged?.Invoke();
            }
        }

        public bool IsLoggedIn => CurrentAccount != null;

        public event Action StateChanged;

        public async Task Login(string username, string password)
        { 
            CurrentAccount = await _authenticationService.Login(username, password);
        }

        public void LogOut()
        {
            CurrentAccount = null;
        }

        public Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword)
        {
            return _authenticationService.Register(email, username, password, confirmPassword);
        }
    }
}
