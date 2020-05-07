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

            //var volunteer = await _dbService.GetVolunteerAsync(1);
            //User.UserId = volunteer.VolunteerId;
            //User.IsManager = false;
            var manager = await _dbService.GetManagerAsync(1);
            User.UserId = manager.ManagerId;
            User.IsManager = true;
            Apply = await _dbService.GetApplyAsync(1);
            ActivityInfo = await _dbService.GetActivityInfoAsync(Apply.ApplyActivityId);
            Manager = await _dbService.GetManagerAsync(Apply.ApplyManagerId);
            TypeName = await _dbService.GetActivityTypeNameAsync(ActivityInfo.ActivityTypeId);

            await SetButtonFunction();
        }

        internal async Task SetButtonFunction() {
            //判断是否为管理员浏览，控制按钮功能
            if (User.IsManager) {
                IsManagerCtrl = true;
                IsSignUp = false;
                IsCancel = false;
            }
            else {
                IsManagerCtrl = false;
                //判断用户是否报名了活动
                if (await _dbService.IsVolunteerInApply(Apply.ApplyId, User.UserId)) {
                    IsSignUp = false;
                    IsCancel = true;
                }
                else {
                    IsCancel = false;
                    //判断该活动是否还能报名
                    IsSignUp = Apply.Status.Equals("报名中");
                }
            }
        }

        private string _typeName;

        public string TypeName {
            get => _typeName;
            set => Set(nameof(TypeName), ref _typeName, value);
        }

        private bool _isSignUp = false;

        public bool IsSignUp {
            get => _isSignUp;
            set => Set(nameof(IsSignUp), ref _isSignUp, value);
        }

        private bool _isCancel = false;

        public bool IsCancel
        {
            get => _isCancel;
            set => Set(nameof(IsCancel), ref _isCancel, value);
        }

        private bool _isManagerCtrl = false;

        public bool IsManagerCtrl
        {
            get => _isManagerCtrl;
            set => Set(nameof(IsManagerCtrl), ref _isManagerCtrl, value);
        }

        private RelayCommand _signUpButtonCommand;

        public RelayCommand SignUpButtonCommand =>
            _signUpButtonCommand ?? (_signUpButtonCommand = new RelayCommand(async () => await SignUpButtonCommandFunction()));

        internal async Task SignUpButtonCommandFunction() {
            await _dbService.AddVolunteerInApply(Apply.ApplyId, User.UserId);
            Apply = await _dbService.GetApplyAsync(Apply.ApplyId);
            await SetButtonFunction();
        }

        private RelayCommand _cancelButtonCommand;

        public RelayCommand CancelButtonCommand =>
            _cancelButtonCommand ?? (_cancelButtonCommand = new RelayCommand(async () => await CancelButtonCommandFunction()));

        internal async Task CancelButtonCommandFunction()
        {
            await _dbService.DeleteVolunteerInApply(Apply.ApplyId, User.UserId);
            Apply = await _dbService.GetApplyAsync(Apply.ApplyId);
            await SetButtonFunction();
        }
    }
}
