using GalaSoft.MvvmLight.Command;
using NEUVolunteer.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.ComponentModel.Design;
using System.Threading.Tasks;
using NEUVolunteer.Models;
using Xamarin.Forms.Internals;

namespace NEUVolunteer.ViewModels
{
    public class HomePageViewModel : NavigationViewModelBase
    {
        public HomePageViewModel(INavigationService navigationService,
            IDBService dBService
            ) : base(
            navigationService)
        {
            _dBService = dBService;
            ItemCollection = new ObservableCollection<ApplyDetail>();
        }
        private IDBService _dBService;
        /// <summary>
        /// if Information page visible
        /// </summary>
        public bool InformationPageVisible
        {
            get => _informationPageVisible;
            set =>
                Set(nameof(InformationPageVisible), ref _informationPageVisible,
                    value);
        }
        private bool _informationPageVisible = true;


        /// <summary>
        /// if collection page visible
        /// </summary>
        public bool ActivityPageVisible
        {
            get => _activityPageVisible;
            set =>
                Set(nameof(ActivityPageVisible), ref _activityPageVisible,
                    value);
        }
        private bool _activityPageVisible;


        /// <summary>
        /// if My page visible
        /// </summary>
        public bool MyPageVisible
        {
            get => _myPageVisible;
            set =>
                Set(nameof(MyPageVisible), ref _myPageVisible,
                    value);
        }
        private bool _myPageVisible;


        /// <summary>
        /// navigation to Information page command
        /// </summary>
        public RelayCommand InformationPageCommand =>
            _informationPageCommand ?? (_informationPageCommand = new RelayCommand(
                async () =>
                {   
                    InformationPageVisible = true;
                    ActivityPageVisible = false;
                    MyPageVisible = false;
                    var news =
                        await _dBService.GetAllNewsAsync();

                    NewsItemCollection.Clear();
                    foreach (var article in news)
                    {

                        NewsItemCollection.Add(new NewsItemViewModel(_navigationService, article));
                    }
                }));
        private RelayCommand _informationPageCommand;


        /// <summary>
        /// navigation to collection page command
        /// </summary>
        public RelayCommand ActivityPageCommand =>
            _activityPageCommand ??
            (_activityPageCommand = new RelayCommand(async () =>
            {
                InformationPageVisible = false;
                ActivityPageVisible = true;
                MyPageVisible = false;
                /*
                var activities =
                    await _dBService.GetActivityInfosAsync();
                ActivityItemCollection.Clear();
                foreach (var article in activities)
                {
                    ActivityItemCollection.Add(new ActivityItemViewModel(_navigationService, article));
                }*/
                await GetItems();
            }));
        private RelayCommand _activityPageCommand;


        /// <summary>
        /// navigation to vocabulary page command
        /// </summary>
        public RelayCommand VocabularyPageCommand =>
            _vocabularyPageCommand ?? (_vocabularyPageCommand =
                new RelayCommand(() =>
                {
                    InformationPageVisible = false;
                    ActivityPageVisible = false;
                    MyPageVisible = true;
                }));
        private RelayCommand _vocabularyPageCommand;


        public RelayCommand AppearCommand =>
            _appearCommand ??
            (_appearCommand = new RelayCommand(async () =>
            {
                if (InformationPageVisible)
                {
                    var news =
                        await _dBService.GetAllNewsAsync();

                    NewsItemCollection.Clear();
                    foreach (var article in news)
                    {

                        NewsItemCollection.Add(new NewsItemViewModel(_navigationService, article));
                    }
                }
                else if (ActivityPageVisible) {
                    /*
                    var activities =
                        await _dBService.GetActivityInfosAsync();
                    ActivityItemCollection.Clear();
                    foreach (var article in activities)
                    {
                        ActivityItemCollection.Add(new ActivityItemViewModel(_navigationService, article));
                    }*/
                    await GetItems();
                }
            }));

        internal async Task GetItems() {
            ItemCollection.Clear();
            var list = await _dBService.GetApplyListAsync();
            foreach (var apply in list) {
                var info = await _dBService.GetActivityInfoAsync(apply.ApplyActivityId);
                ItemCollection.Add(new ApplyDetail(apply, info));
            }
        }

        private RelayCommand _appearCommand;

        public ObservableCollection<NewsItemViewModel>
            NewsItemCollection
        { get; } = new ObservableCollection<NewsItemViewModel>();

        public ObservableCollection<ActivityItemViewModel> ActivityItemCollection
        {
            get;
        } = new ObservableCollection<ActivityItemViewModel>();

        public ObservableCollection<ApplyDetail> ItemCollection { get; set; }

        private RelayCommand<ApplyDetail> _applyItemTappedCommand;

        public RelayCommand<ApplyDetail> ApplyItemTappedCommand =>
            _applyItemTappedCommand ?? (_applyItemTappedCommand = new RelayCommand<ApplyDetail>(detail => ApplyItemTappedCommandFunction(detail)));

        internal void ApplyItemTappedCommandFunction(ApplyDetail detail) {
           _navigationService.NavigationTo(NavigationServiceConstants.ActivityDetailPage, detail);
        }


    }
}
