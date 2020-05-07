using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NEUVolunteer.Models;
using NEUVolunteer.Services.interfaces;

namespace NEUVolunteer.ViewModels
{
    public class UpdateApplyPageViewModel : ViewModelBase
    {
        public UpdateApplyPageViewModel(IDBService dbService) {
            _dbService = dbService;
        }

        private IDBService _dbService;

        private Apply _apply;

        public Apply Apply {
            get => _apply;
            set => Set(nameof(Apply), ref _apply, value);
        }

        private RelayCommand _pageAppearingCommand;

        public RelayCommand PageAppearingCommand =>
            _pageAppearingCommand ?? (_pageAppearingCommand =
                new RelayCommand(async () => await PageAppearingCommandFunction()));

        internal async Task PageAppearingCommandFunction() {
            Apply = await _dbService.GetApplyAsync(2);

            StartDate = StringToDateTime(Apply.StartTime);
            StartTime = StringToTimeSpan(Apply.StartTime);
            EndDate = StringToDateTime(Apply.EndTime);
            EndTime = StringToTimeSpan(Apply.EndTime);
            GatherDate = StringToDateTime(Apply.GatherTime);
            GatherTime = StringToTimeSpan(Apply.GatherTime);
        }

        private DateTime _startDate;

        public DateTime StartDate
        {
            get => _startDate;
            set => Set(nameof(StartDate), ref _startDate, value);
        }


        private TimeSpan _startTime;

        public TimeSpan StartTime
        {
            get => _startTime;
            set => Set(nameof(StartTime), ref _startTime, value);
        }

        private DateTime _endDate;

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

        private DateTime _gatherDate;

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

        private RelayCommand _updateButtonCommand;

        public RelayCommand UpdateButtonCommand =>
            _updateButtonCommand ?? (_updateButtonCommand = new RelayCommand(
                async () => await UpdateButtonCommandFunction()));

        internal async Task UpdateButtonCommandFunction() {
            Apply.StartTime = TimeToString(StartDate, StartTime);
            Apply.EndTime = TimeToString(EndDate, EndTime);
            Apply.GatherTime = TimeToString(GatherDate, GatherTime);
            await _dbService.UpdateApplyAsync(Apply);
        }

        internal string TimeToString(DateTime date, TimeSpan time)
        {
            var s = date.Year + "年" + date.Month + "月" + date.Day + "日";
            switch (date.DayOfWeek.ToString())
            {
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

        internal DateTime StringToDateTime(string date) {
            var flag = 'y';
            var year = "";
            var month = "";
            var day = "";
            for (int i = 0; i < date.Length; i++) {
                if (date[i] == '年') {
                    flag = 'm';
                    continue;
                } else if (date[i] == '月') {
                    flag = 'd';
                    continue;
                } else if (date[i] == '日') {
                    break;
                }

                if (flag == 'y') {
                    year += date[i];
                } else if (flag == 'm') {
                    month += date[i];
                } else if (flag == 'd') {
                    day += date[i];
                }
            }
            DateTime d = new DateTime(int.Parse(year), int.Parse(month), int.Parse(day));
            return d;
        }

        internal TimeSpan StringToTimeSpan(string time) {
            var flag = 'w';
            var hour = "";
            var min = "";
            for (int i = 0; i < time.Length; i++) {
                if (time[i] == ' ') {
                    flag =  'h';
                    continue;
                }
                
                if (flag == 'w') {
                    continue;
                } else if (flag == 'h'){
                    if (time[i] != ':')
                    {
                        hour += time[i];
                    } else {
                        flag = 'm';
                        continue;
                    }
                } else if (flag == 'm') {
                    min += time[i];
                }
            }
            TimeSpan t = new TimeSpan(int.Parse(hour), int.Parse(min), 0);
            return t;
        }
    }
}
