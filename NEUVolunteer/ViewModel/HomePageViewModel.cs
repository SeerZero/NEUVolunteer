using GalaSoft.MvvmLight.Command;
using NEUVolunteer.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NEUVolunteer.ViewModel
{
    public class HomePageViewModel : NavigationViewModelBase
    {
        public HomePageViewModel(INavigationService navigationService,
            IAlertService alertService
            ) : base(
            navigationService)
        {
            _alertService = alertService;
        }
        private IAlertService _alertService;
        /// <summary>
        /// if recommend page visible
        /// </summary>
        public bool RecommendPageVisible
        {
            get => _recommendPageVisible;
            set =>
                Set(nameof(RecommendPageVisible), ref _recommendPageVisible,
                    value);
        }
        private bool _recommendPageVisible = true;


        /// <summary>
        /// if collection page visible
        /// </summary>
        public bool CollectionPageVisible
        {
            get => _collectionPageVisible;
            set =>
                Set(nameof(CollectionPageVisible), ref _collectionPageVisible,
                    value);
        }
        private bool _collectionPageVisible;


        /// <summary>
        /// if vocabulary page visible
        /// </summary>
        public bool VocabularyPageVisible
        {
            get => _vocabularyPageVisible;
            set =>
                Set(nameof(VocabularyPageVisible), ref _vocabularyPageVisible,
                    value);
        }
        private bool _vocabularyPageVisible;
    }
}
