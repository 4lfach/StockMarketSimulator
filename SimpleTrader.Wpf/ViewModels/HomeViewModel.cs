using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.Wpf.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public AssetSummaryViewModel AssetSummaryViewModel { get; }
        public MajorIndexListingViewModel MajorIndexListingViewModel { get; set; }

        public HomeViewModel(AssetSummaryViewModel assetSummaryViewModel, MajorIndexListingViewModel majorIndexListingViewModel)
        {
            MajorIndexListingViewModel = majorIndexListingViewModel;
            AssetSummaryViewModel = assetSummaryViewModel;
        }

        public override void Dispose()
        {
            AssetSummaryViewModel.Dispose();
            MajorIndexListingViewModel.Dispose();

            base.Dispose();
        }
    }
}
