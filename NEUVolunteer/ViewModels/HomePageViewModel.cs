using GalaSoft.MvvmLight.Command;
using NEUVolunteer.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.ComponentModel.Design;
using System.Threading.Tasks;
using NEUVolunteer.Models;
using Xamarin.Essentials;
using Xamarin.Forms.Internals;

namespace NEUVolunteer.ViewModels
{
    public class HomePageViewModel : NavigationViewModelBase
    {
        Volunteer _volunteer;

        public HomePageViewModel(INavigationService navigationService,
            IDBService dBService
            ) : base(
            navigationService)
        {
            _dBService = dBService;
            AllApplyDetailCollection = new ObservableCollection<ApplyDetail>();
            MyApplyDetailCollection = new ObservableCollection<ApplyDetail>();
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
        private bool _informationPageVisible = false;


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
        private bool _activityPageVisible = true;


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
        private bool _myPageVisible = false;


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
                await GetAllApplyDetail();
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
                int s = Preferences.Get("User", 0);
                _volunteer = await _dBService.GetVolunteerAsync(Preferences.Get("User", 0));

                UserName = _volunteer.VolunteerName;
                UserNumber = _volunteer.VolunteerPhone;
                UserQQ = _volunteer.VolunteerQQ;
                UserSex = _volunteer.VolunteerSex;

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
                    await GetAllApplyDetail();
                }
                else {
                    await GetMyApplyDetail();
                }
            }));

        internal async Task GetAllApplyDetail() {
            AllApplyDetailCollection.Clear();
            var list = await _dBService.GetApplyListAsync();
            foreach (var apply in list) {
                var info = await _dBService.GetActivityInfoAsync(apply.ApplyActivityId);
                AllApplyDetailCollection.Add(new ApplyDetail(apply, info));
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

        public ObservableCollection<ApplyDetail> AllApplyDetailCollection { get; set; }

        private RelayCommand<ApplyDetail> _applyItemTappedCommand;

        public RelayCommand<ApplyDetail> ApplyItemTappedCommand =>
            _applyItemTappedCommand ?? (_applyItemTappedCommand = new RelayCommand<ApplyDetail>(detail => ApplyItemTappedCommandFunction(detail)));

        internal void ApplyItemTappedCommandFunction(ApplyDetail detail) {
           _navigationService.NavigationTo(NavigationServiceConstants.ActivityDetailPage, detail);
        }


        //***************我的**************************

        public string UserSex
        {
            get => _userSex;
            set =>
                Set(nameof(UserSex), ref _userSex,
                    value);
        }
        private string _userSex;
        public long UserNumber { 
            get => _userNumber;
            set => Set(nameof(UserNumber), ref _userNumber,
                    value);
        }
        private long _userNumber;
        public long UserQQ {
            get => _userQQ;
            set => Set(nameof(UserQQ), ref _userQQ, value);
        }
        private long _userQQ;
        public string UserName {
            get => _userName;
            set => Set(nameof(UserName), ref _userName, value);
        }
        private string _userName;

        public ObservableCollection<ApplyDetail> MyApplyDetailCollection { get; set; }

        public RelayCommand QuitCommand =>
            _quitCommand ?? (_quitCommand = new RelayCommand(
                () =>
                {
                    _navigationService.NavigationTo(NavigationServiceConstants.LoginPage, false);
                }));
        private RelayCommand _quitCommand;

        public RelayCommand RenewCommand =>
            _renewCommand ?? (_renewCommand = new RelayCommand(
                () =>
                {

                }));
        private RelayCommand _renewCommand;

        public RelayCommand MyPageCommand =>
            _myPageCommand ?? (_myPageCommand =
                new RelayCommand(async () => await MyPageCommandFunction()));

        private async Task MyPageCommandFunction() {
            InformationPageVisible = false;
            ActivityPageVisible = false;
            MyPageVisible = true;

            await GetMyApplyDetail();
        }

        private RelayCommand _myPageCommand;

        internal async Task GetMyApplyDetail()
        {
            MyApplyDetailCollection.Clear();
            var list = await _dBService.GetMyApplyAsync(); 
            foreach (var apply in list) {
                var info = await _dBService.GetActivityInfoAsync(apply.ApplyActivityId);
                MyApplyDetailCollection.Add(new ApplyDetail(apply, info));
            }
        }

    }
}
