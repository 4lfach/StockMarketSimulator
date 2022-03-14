using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Wpf.States.Authenticators;
using SimpleTrader.Wpf.States.Navigators;
using SimpleTrader.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleTrader.Wpf.Commands
{
    public class LoginCommand : AsyncCommandBase
    {
        public event EventHandler CanExecuteChanged;

        private readonly LoginViewModel _viewModel;
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _renavigator;

        public LoginCommand(LoginViewModel viewModel, IAuthenticator authenticator, IRenavigator renavigator)
        {
            _viewModel = viewModel;
            _authenticator = authenticator;
            _renavigator = renavigator;

            _viewModel.PropertyChanged += LoginViewModel_PropertyChanged;
        }

        private void LoginViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(_viewModel.CanLogin))
            {
                OnCanExecuteChanged();
            }
        }

        public async override Task ExecuteAsync(object parameter)
        {
            try
            {
                await _authenticator.Login(_viewModel.Username, _viewModel.Password);

                _renavigator.Renavigate();
            }
            catch (UserNotFoundException)
            {
                _viewModel.ErrorMessage = "The username does not exist.";
            }
            catch (InvalidPasswordException)
            {
                _viewModel.ErrorMessage = "Incorrect password";
            }
            catch (Exception)
            {
                _viewModel.ErrorMessage = "Log in failed.";
            }
        }

        public override bool CanExecute(object parameter)
        {
            return _viewModel.CanLogin && base.CanExecute(parameter);
        }
    }
}
