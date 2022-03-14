using SimpleTrader.Wpf.Commands;
using SimpleTrader.Wpf.States.Authenticators;
using SimpleTrader.Wpf.States.Navigators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SimpleTrader.Wpf.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        private string _email;
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
                OnPropertyChanged(nameof(CanRegister));
            }
        }
        private string _username;
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
                OnPropertyChanged(nameof(CanRegister));
            }
        }
        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
                OnPropertyChanged(nameof(CanRegister));
            }
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get
            {
                return _confirmPassword;
            }
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
                OnPropertyChanged(nameof(CanRegister));
            }
        }

        public bool CanRegister =>  !string.IsNullOrEmpty(_username) &&
            !string.IsNullOrEmpty(_email) &&
            !string.IsNullOrEmpty(_password) &&
            !string.IsNullOrEmpty(_confirmPassword);

        public ICommand RegisterCommand { get; }
        public ICommand ViewLoginCommand { get; }
        public MessageViewModel ErrorMessageViewModel { get; }

        public string ErrorMessage
        {
            set { ErrorMessageViewModel.Message = value; }
        }

        public RegisterViewModel(IAuthenticator authenticator, IRenavigator registerRenavigator,IRenavigator loginRenavigator)
        {
            ErrorMessageViewModel = new MessageViewModel();

            ViewLoginCommand = new RenavigateCommand(loginRenavigator);
            RegisterCommand = new RegisterCommand(this, authenticator, registerRenavigator);
        }
    }
}
