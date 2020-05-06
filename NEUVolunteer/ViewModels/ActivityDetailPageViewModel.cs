using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NEUVolunteer.Models;
using NEUVolunteer.Services.interfaces;

namespace NEUVolunteer.ViewModels
{
    public class ActivityDetailPageViewModel : ViewModelBase{

        public ActivityDetailPageViewModel(IDBService dbService) {
            _dbService = dbService;
        }

        private IDBService _dbService;

        private Apply _apply;

        public Apply Apply
        {
            get => _apply;
            set => Set(nameof(Apply), ref _apply, value);
        }

        private ActivityInfo _activityInfo;

        public ActivityInfo ActivityInfo {
            get => _activityInfo;
            set => Set(nameof(ActivityInfo), ref _activityInfo, value);
        }

        private Manager _manager;

        public Manager Manager {
            get => _manager;
            set => Set(nameof(Manager), ref _manager, value);
        }

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

            Apply = await _dbService.GetApplyAsync(1);
            ActivityInfo = await _dbService.GetActivityInfoAsync(Apply.ApplyActivityId);
            Manager = await _dbService.GetManagerAsync(Apply.ApplyManagerId);
            TypeName = await _dbService.GetActivityTypeNameAsync(ActivityInfo.ActivityTypeId);
        }

        private string _typeName;

        public string TypeName {
            get => _typeName;
            set => Set(nameof(TypeName), ref _typeName, value);
        }
    }
}
