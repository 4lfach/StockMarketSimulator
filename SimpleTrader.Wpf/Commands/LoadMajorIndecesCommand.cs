using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.Wpf.Commands
{
    public class LoadMajorIndecesCommand : AsyncCommandBase
    {
        private readonly MajorIndexListingViewModel _majorIndexListingViewModel;
        private readonly IMajorIndexService _majorIndexService;

        public LoadMajorIndecesCommand(MajorIndexListingViewModel majorIndexListingViewModel, IMajorIndexService majorIndexService)
        {
            _majorIndexListingViewModel = majorIndexListingViewModel;
            _majorIndexService = majorIndexService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _majorIndexListingViewModel.IsLoading = true;

            await Task.WhenAll(LoadDowJones(), LoadNasdaq(), LoadSP500());

            _majorIndexListingViewModel.IsLoading = false;
        }

        private async Task LoadDowJones()
        {
            _majorIndexListingViewModel.DowJones = await _majorIndexService.GetMajorIndex(MajorIndexType.DowJones);
        }

        private async Task LoadNasdaq()
        {
            _majorIndexListingViewModel.Nasdaq = await _majorIndexService.GetMajorIndex(MajorIndexType.Nasdaq);
        }
        private async Task LoadSP500()
        {
            _majorIndexListingViewModel.SP500 = await _majorIndexService.GetMajorIndex(MajorIndexType.SP500);
        }
    }
}
