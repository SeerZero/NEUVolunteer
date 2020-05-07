using GalaSoft.MvvmLight.Command;
using NEUVolunteer.Model;
using NEUVolunteer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace NEUVolunteer.ViewModel
{
    public class NewsItemViewModel : NavigationViewModelBase
    {


        public News Newss
        {
            get => _newss;
            set => Set(nameof(Newss), ref _newss, value);
        }
        private News _newss;

        public NewsItemViewModel(INavigationService navigationService, News news) :
            base(navigationService)
        {
            Newss = news;
        }

        public RelayCommand SelectCommand =>
            _selectCommand ?? (_selectCommand = new RelayCommand(() => {
                _navigationService.NavigationTo(NavigationServiceConstants.NewsReadPage, Newss);
            }));
        private RelayCommand _selectCommand;
    }
}
