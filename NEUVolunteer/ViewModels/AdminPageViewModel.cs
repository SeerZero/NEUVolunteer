using NEUVolunteer.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using NEUVolunteer.Models;

namespace NEUVolunteer.ViewModels
{
    public class AdminPageViewModel : NavigationViewModelBase
    {
        public AdminPageViewModel(INavigationService navigationService, IDBService dBService) : base(navigationService)
        {
            _dBService = dBService;
            AllApplyDetailCollection = new ObservableCollection<ApplyDetail>();
        }

        private IDBService _dBService;

        public ObservableCollection<ApplyDetail> AllApplyDetailCollection { get; set; }

        internal async Task GetAllApplyDetail()
        {
            AllApplyDetailCollection.Clear();
            var list = await _dBService.GetApplyListAsync();
            foreach (var apply in list)
            {
                var info = await _dBService.GetActivityInfoAsync(apply.ApplyActivityId);
                AllApplyDetailCollection.Add(new ApplyDetail(apply, info));
            }
        }

        private RelayCommand _pageAppearingCommand;

        public RelayCommand PageAppearingCommand =>
            _pageAppearingCommand ?? (_pageAppearingCommand =
                new RelayCommand(async () => await PageAppearingCommandFunction()));

        internal async Task PageAppearingCommandFunction() {
            await GetAllApplyDetail();
        }

        private RelayCommand _createActivityCommand;

        public RelayCommand CreateActivityCommand =>
            _createActivityCommand ?? (_createActivityCommand = new RelayCommand(() => {
                _navigationService.NavigationTo(NavigationServiceConstants.CreateActivityPage);
            }));

        private RelayCommand _createApplyCommand;

        public RelayCommand CreateApplyCommand =>
            _createApplyCommand ?? (_createApplyCommand = new RelayCommand(() => {
                _navigationService.NavigationTo(NavigationServiceConstants.CreateApplyPage);
            }));

        private RelayCommand _quitCommand;

        public RelayCommand QuitCommand =>
            _quitCommand ?? (_quitCommand = new RelayCommand(() => {
                _navigationService.NavigationTo(NavigationServiceConstants.LoginPage, false);
            }));

        private RelayCommand<ApplyDetail> _applyItemTappedCommand;

        public RelayCommand<ApplyDetail> ApplyItemTappedCommand =>
            _applyItemTappedCommand ?? (_applyItemTappedCommand = new RelayCommand<ApplyDetail>(detail => ApplyItemTappedCommandFunction(detail)));

        internal void ApplyItemTappedCommandFunction(ApplyDetail detail)
        {
            _navigationService.NavigationTo(NavigationServiceConstants.ActivityDetailPage, detail);
        }
    }
}
