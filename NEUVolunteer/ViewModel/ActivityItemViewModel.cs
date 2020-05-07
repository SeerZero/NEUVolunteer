using GalaSoft.MvvmLight.Command;
using NEUVolunteer.Model;
using NEUVolunteer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace NEUVolunteer.ViewModel
{ 
    public class ActivityItemViewModel : NavigationViewModelBase
    {
        public ActivityInfo Activitys
        {
            get => _activitys;
            set => Set(nameof(Activitys), ref _activitys, value);
        }
        private ActivityInfo _activitys;
             
        public ActivityItemViewModel(INavigationService navigationService, ActivityInfo activity) :
            base(navigationService)
        {
            Activitys = activity;
        }

        public RelayCommand SelectCommand =>
            _selectCommand ?? (_selectCommand = new RelayCommand(() => {
                _navigationService.NavigationTo(NavigationServiceConstants.ActivityReadPage, Activitys);
            }));
        private RelayCommand _selectCommand;
    }
}
