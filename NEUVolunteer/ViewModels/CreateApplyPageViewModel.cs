using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NEUVolunteer.Models;
using NEUVolunteer.Services.interfaces;
using Xamarin.Forms.Internals;

namespace NEUVolunteer.ViewModels
{
    public class CreateApplyPageViewModel : ViewModelBase
    {
        public CreateApplyPageViewModel(IDBService dbService)
        {
            _dbService = dbService;
            ActivityInfos = new ObservableCollection<ActivityInfo>();
        }

        private IDBService _dbService;

        public ObservableCollection<ActivityInfo> ActivityInfos { get; set; }

        private RelayCommand _pageAppearingCommand;

        public RelayCommand PageAppearingCommand =>
            _pageAppearingCommand ?? (_pageAppearingCommand =
                new RelayCommand(async () => await PageAppearingCommandFunction()));

        internal async Task PageAppearingCommandFunction()
        {
            if (!_dbService.Initialized())
            {
                await _dbService.InitializeAsync();
            }

            User.IsManager = true;
            User.UserId = 1;

            var infosList = await _dbService.GetActivityInfosAsync();
            infosList.ForEach(p => ActivityInfos.Add(p));
        }

        private int _activityId;

        public int ActivityId {
            get => _activityId;
            set => Set(nameof(ActivityId), ref _activityId, value);
        }

        private DateTime _startDate = DateTime.Today;

        public DateTime StartDate {
            get => _startDate;
            set => Set(nameof(StartDate), ref _startDate, value);
        }


        private TimeSpan _startTime;

        public TimeSpan StartTime
        {
            get => _startTime;
            set => Set(nameof(StartTime), ref _startTime, value);
        }

        private DateTime _endDate = DateTime.Today;

        public DateTime EndDate
        {
            get => _endDate;
            set => Set(nameof(EndDate), ref _endDate, value);
        }


        private TimeSpan _endTime;

        public TimeSpan EndTime
        {
            get => _endTime;
            set => Set(nameof(EndTime), ref _endTime, value);
        }

        private DateTime _gatherDate = DateTime.Today;

        public DateTime GatherDate
        {
            get => _gatherDate;
            set => Set(nameof(GatherDate), ref _gatherDate, value);
        }


        private TimeSpan _gatherTime;

        public TimeSpan GatherTime
        {
            get => _gatherTime;
            set => Set(nameof(GatherTime), ref _gatherTime, value);
        }

        private string _gatherPlace;

        public string GatherPlace {
            get => _gatherPlace;
            set => Set(nameof(GatherPlace), ref _gatherPlace, value);
        }

        private int _requestNumber;

        public int RequestNumber
        {
            get => _requestNumber;
            set => Set(nameof(RequestNumber), ref _requestNumber, value);
        }

        private RelayCommand _createButtonCommand;

        public RelayCommand CreateButtonCommand =>
            _createButtonCommand ?? (_createButtonCommand = new RelayCommand(async () => await CreateButtonCommandFunction()));

        internal async Task CreateButtonCommandFunction() {
            var gather = TimeToString(GatherDate, GatherTime);
            var start = TimeToString(StartDate, StartTime);
            var end = TimeToString(EndDate, EndTime);
            await _dbService.AddApplyAsync(ActivityId + 1, User.UserId, 
                gather, GatherPlace, start, end, RequestNumber);
        }

        internal string TimeToString(DateTime date, TimeSpan time) {
            var s = date.Year + "年" + date.Month + "月" + date.Day + "日";
            switch (date.DayOfWeek.ToString()) {
                case "Sunday":
                    s += "(周日) "; break;
                case "Monday":
                    s += "(周一) "; break;
                case "Tuesday":
                    s += "(周二) "; break;
                case "Wednesday":
                    s += "(周三) "; break;
                case "Thursday":
                    s += "(周四) "; break;
                case "Friday":
                    s += "(周五) "; break;
                case "Saturday":
                    s += "(周六) "; break;
                default:
                    break;
            }

            if (time.Hours == 0) 
                s += "00";
            else 
                s += time.Hours.ToString();
            
            s += ":";

            if (time.Minutes == 0)
                s += "00";
            else
                s += time.Minutes.ToString();

            return s;
        }

        
    }
}