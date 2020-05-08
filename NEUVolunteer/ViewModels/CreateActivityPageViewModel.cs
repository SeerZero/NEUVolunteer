using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NEUVolunteer.Models;
using NEUVolunteer.Services;
using Xamarin.Forms.Internals;

namespace NEUVolunteer.ViewModels
{
    public class CreateActivityPageViewModel : ViewModelBase {

        public CreateActivityPageViewModel(IDBService dbService) {
            _dbService = dbService;
            ActivityTypes = new ObservableCollection<ActivityType>();
        }

        private IDBService _dbService;

        public ObservableCollection<ActivityType> ActivityTypes { get; set; }

        private RelayCommand _pageAppearingCommand;

        public RelayCommand PageAppearingCommand =>
            _pageAppearingCommand ?? (_pageAppearingCommand = 
                new RelayCommand(async () => await PageAppearingCommandFunction()));

        internal async Task PageAppearingCommandFunction() {
            if (!_dbService.Initialized()) {
                await _dbService.InitializeAsync();
            }

            var typesList =  await _dbService.GetActivityTypesAsync();
            typesList.ForEach(p => ActivityTypes.Add(p));
        }

        private string _activityName;

        public string ActivityName {
            get => _activityName;
            set => Set(nameof(ActivityName), ref _activityName, value);
        }

        private int _activityType;

        public int ActivityType
        {
            get => _activityType;
            set => Set(nameof(ActivityType), ref _activityType, value);
        }
        

        private string _activityPlace;

        public string ActivityPlace
        {
            get => _activityPlace;
            set => Set(nameof(ActivityPlace), ref _activityPlace, value);
        }

        private string _activityBrief;

        public string ActivityBrief
        {
            get => _activityBrief;
            set => Set(nameof(ActivityBrief), ref _activityBrief, value);
        }

        private RelayCommand _createButtonCommand;

        public RelayCommand CreateButtonCommand =>
            _createButtonCommand ?? (_createButtonCommand = new RelayCommand(async () => await CreateButtonCommandFunction()));

        internal async Task CreateButtonCommandFunction() {
            Console.WriteLine(ActivityName + "\n" + ActivityType + "\n" + ActivityPlace + "\n" + ActivityBrief);
            await _dbService.AddActivityInfo(ActivityName, ActivityPlace, ActivityBrief, ActivityType + 1);
        }

    }
}
