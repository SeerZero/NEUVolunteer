using GalaSoft.MvvmLight;
using NEUVolunteer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace NEUVolunteer.ViewModels
{
    public abstract class NavigationViewModelBase : ViewModelBase
    {
        protected NavigationViewModelBase(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        protected readonly INavigationService _navigationService;
    }
}
