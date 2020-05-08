using GalaSoft.MvvmLight.Command;
using NEUVolunteer.Services;
using System;
using System.Collections.Generic;
using System.Text;
using NEUVolunteer.Models;

namespace NEUVolunteer.ViewModels
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
