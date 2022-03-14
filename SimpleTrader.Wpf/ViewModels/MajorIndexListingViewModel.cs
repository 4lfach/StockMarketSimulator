﻿using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.Wpf.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleTrader.Wpf.ViewModels
{
    public class MajorIndexListingViewModel : ViewModelBase
    {
        private readonly IMajorIndexService _majorIndexService;

        private MajorIndex _dowJones;

        public MajorIndex DowJones
        {
            get { return _dowJones; }
            set { _dowJones = value; OnPropertyChanged(nameof(DowJones)); }
        }

        private MajorIndex _nasdaq;

        public MajorIndex Nasdaq
        {
            get { return _nasdaq; }
            set { _nasdaq = value; OnPropertyChanged(nameof(Nasdaq)); }
        }
        private MajorIndex _sp500;

        public MajorIndex SP500
        {
            get { return _sp500; }
            set { _sp500 = value; OnPropertyChanged(nameof(SP500)); }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        public ICommand LoadMajorIndecesCommand { get; }

        public MajorIndexListingViewModel(IMajorIndexService majorIndexService)
        {
            _majorIndexService = majorIndexService;

            LoadMajorIndecesCommand = new LoadMajorIndecesCommand(this, majorIndexService);
        }

        public static MajorIndexListingViewModel LoadMajorIndexViewModel(IMajorIndexService majorIndexService)
        {
            MajorIndexListingViewModel majorIndexViewModel = new MajorIndexListingViewModel(majorIndexService);

            majorIndexViewModel.LoadMajorIndecesCommand.Execute(null);

            return majorIndexViewModel;
        }

    }
}
