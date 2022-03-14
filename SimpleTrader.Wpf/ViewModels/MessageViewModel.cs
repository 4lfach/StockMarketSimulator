﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.Wpf.ViewModels
{
    public class MessageViewModel :ViewModelBase
    {
        private string _message;

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
                OnPropertyChanged(nameof(HasMessage));
            }
        }

        public bool HasMessage => _message != null;
    }
}
