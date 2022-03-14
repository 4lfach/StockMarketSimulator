using SimpleTrader.Wpf.States.Navigators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SimpleTrader.Wpf.Commands
{
    public class RenavigateCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly IRenavigator _renavigator;

        public RenavigateCommand(IRenavigator renavigator)
        {
            _renavigator = renavigator;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _renavigator.Renavigate();
        }
    }
}
