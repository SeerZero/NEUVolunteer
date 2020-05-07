using GalaSoft.MvvmLight;
using NEUVolunteer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace NEUVolunteer.ViewModel
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
