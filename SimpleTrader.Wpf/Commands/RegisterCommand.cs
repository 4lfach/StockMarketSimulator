using SimpleTrader.Domain.Services.AuthenticationServices;
using SimpleTrader.Wpf.States.Authenticators;
using SimpleTrader.Wpf.States.Navigators;
using SimpleTrader.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.Wpf.Commands
{
    public class RegisterCommand : AsyncCommandBase
    {
        private readonly RegisterViewModel _registerViewModel;
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _registerRenavigator;

        public RegisterCommand(RegisterViewModel registerViewModel, IAuthenticator authenticator, IRenavigator renavigator)
        {
            _registerViewModel = registerViewModel;
            _authenticator = authenticator;
            _registerRenavigator = renavigator;

            _registerViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(RegisterViewModel.CanRegister))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return _registerViewModel.CanRegister && base.CanExecute(parameter);
        }


        public override async Task ExecuteAsync(object parameter)
        {
            _registerViewModel.ErrorMessage = string.Empty;

            try
            {
                RegistrationResult result = await _authenticator.Register(_registerViewModel.Email,
                            _registerViewModel.Username,
                            _registerViewModel.Password,
                            _registerViewModel.ConfirmPassword);

                switch (result)
                {
                    case RegistrationResult.Success:
                        _registerRenavigator.Renavigate();
                        break;
                    case RegistrationResult.PasswordsDoNotMatch:
                        _registerViewModel.ErrorMessage = "Passwords do not match.";
                        break;
                    case RegistrationResult.EmailAlreadyExists:
                        _registerViewModel.ErrorMessage = "Email already exists.";
                        break;
                    case RegistrationResult.UsernameAlreadyExists:
                        _registerViewModel.ErrorMessage = "User name already exists.";
                        break;
                    default:
                        _registerViewModel.ErrorMessage = "Ooops, something went wrong(";
                        break;
                }
            }
            catch (Exception)
            {
                _registerViewModel.ErrorMessage = "Ooops, something went wrong(";
            }
        }
    }
}
