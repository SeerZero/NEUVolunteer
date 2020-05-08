using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NEUVolunteer.Models;
using NEUVolunteer.Services;

namespace NEUVolunteer.ViewModels
{
    public class ActivityDetailPageViewModel : ViewModelBase{

        public ActivityDetailPageViewModel(IDBService dbService) {
            _dbService = dbService;
        }

        private IDBService _dbService;

        private ApplyDetail _applyDetail;

        public ApplyDetail ApplyDetail {
            get => _applyDetail;
            set => Set(nameof(ApplyDetail), ref _applyDetail, value);
        }

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

            var volunteer = await _dbService.GetVolunteerAsync(1);
            User.UserId = volunteer.VolunteerId;
            User.IsManager = false;
            //var manager = await _dbService.GetManagerAsync(1);
            //User.UserId = manager.ManagerId;
            //User.IsManager = true;
            Apply = ApplyDetail.Apply;
            ActivityInfo = ApplyDetail.Info;
            Manager = await _dbService.GetManagerAsync(Apply.ApplyManagerId);
            TypeName = await _dbService.GetActivityTypeNameAsync(ActivityInfo.ActivityTypeId);

            await SetButtonFunction();
        }

        internal async Task SetButtonFunction() {
            //判断是否为管理员浏览，控制按钮功能
            if (User.IsManager) {
                IsManagerCtrl = true;
                IsVolunteerCtrl = false;
                if (Apply.Status == "报名截止") {
                    IsRestore = true;
                    IsStop = false;
                }
                else {
                    IsStop = true;
                    IsRestore = false;
                }
            }
            else {
                IsManagerCtrl = false;
                IsVolunteerCtrl = true;
                //判断用户是否报名了活动
                if (await _dbService.IsVolunteerInApply(Apply.ApplyId, User.UserId)) {
                    if (Apply.Status != "报名截止") {
                        IsSignUp = false;
                        IsCancel = true;
                        IsTip = false;
                    }
                    else {
                        IsSignUp = false;
                        IsCancel = false;
                        IsTip = true;
                    }
                }
                else {
                    IsCancel = false;
                    IsTip = false;
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

        private bool _isTip = false;

        public bool IsTip
        {
            get => _isTip;
            set => Set(nameof(IsTip), ref _isTip, value);
        }

        private bool _isManagerCtrl = false;

        public bool IsManagerCtrl
        {
            get => _isManagerCtrl;
            set => Set(nameof(IsManagerCtrl), ref _isManagerCtrl, value);
        }

        private bool _isVolunteerCtrl = false;

        public bool IsVolunteerCtrl
        {
            get => _isVolunteerCtrl;
            set => Set(nameof(IsVolunteerCtrl), ref _isVolunteerCtrl, value);
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

        private bool _isStop;

        public bool IsStop
        {
            get => _isStop;
            set => Set(nameof(IsStop), ref _isStop, value);
        }

        private bool _isRestore = false;

        public bool IsRestore
        {
            get => _isRestore;
            set => Set(nameof(IsRestore), ref _isRestore, value);
        }

        private RelayCommand _stopButtonCommand;

        public RelayCommand StopButtonCommand =>
            _stopButtonCommand ?? (_stopButtonCommand = new RelayCommand(
                async () => await StopButtonCommandFunction()));

        internal async Task StopButtonCommandFunction() {
            await _dbService.StopApplyAsync(Apply.ApplyId);
            Apply = await _dbService.GetApplyAsync(Apply.ApplyId);
            await SetButtonFunction();
        }

        private RelayCommand _restoreButtonCommand;

        public RelayCommand RestoreButtonCommand =>
            _restoreButtonCommand ?? (_restoreButtonCommand = new RelayCommand(
                async () => await RestoreButtonCommandFunction()));

        internal async Task RestoreButtonCommandFunction()
        {
            await _dbService.RestoreApplyAsync(Apply.ApplyId);
            Apply = await _dbService.GetApplyAsync(Apply.ApplyId);
            await SetButtonFunction();
        }

    }
}
