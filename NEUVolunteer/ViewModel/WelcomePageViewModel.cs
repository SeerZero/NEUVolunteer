using GalaSoft.MvvmLight.Command;
using NEUVolunteer.Model;
using NEUVolunteer.Service;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NEUVolunteer.ViewModel
{
    public class WelcomePageViewModel : NavigationViewModelBase
    {
        public WelcomePageViewModel(INavigationService navigationService,
            ITodayImageService todayImageService,
            IMottoService mottoService) : base(
            navigationService)
        {
            _todayImageService = todayImageService;
            _imottoService = mottoService;
        }
        private IMottoService _imottoService; 
        public bool FirstPageVisible
        {
            get => _firstPageVisible;
            set => Set(nameof(FirstPageVisible), ref _firstPageVisible, value);
        }
        private bool _firstPageVisible = true;

        public bool SecondPageVisible
        {
            get => _secondPageVisible;
            set =>
                Set(nameof(SecondPageVisible), ref _secondPageVisible, value);
        }
        private bool _secondPageVisible;

        public RelayCommand AppearCommand =>
        _appearCommand ?? (_appearCommand = new RelayCommand(() =>
        {
            Task.Run(async () =>
            {

                await Task.Run(() => Thread.Sleep(4000));
                Device.BeginInvokeOnMainThread(async () =>
                {
                    TodaySentence = await _imottoService.CheckUpdateAsync();
                    TodayImage = await _todayImageService.GetTodayImageAsync();
                    var checkUpdateResult =
                        await _todayImageService.CheckUpdateAsync();
                    if (checkUpdateResult.HasUpdate)
                        TodayImage = checkUpdateResult.TodayImage;
                    FirstPageVisible = false;
                    SecondPageVisible = true;
                });
                await Task.Run(() => Thread.Sleep(4000));
          //      string pageKey;
                if (Preferences.Get("FirstLogin", true))
               // if(true)
                {
                    Preferences.Set("FirstLogin", false);
               //     Preferences.Set(WordDatabaseConstants.Key,"0");
               //    pageKey = NavigationServiceConstants
                //       .VocabularyTestConfirmPage;
                }
                else
                {
          //          Word.wordNumber = int.Parse(Preferences.Get(WordDatabaseConstants.Key, ""));
           //         pageKey = NavigationServiceConstants.HomePage;
                }
           ///     Device.BeginInvokeOnMainThread(() =>
            //        _navigationService.NavigationTo(pageKey, false));

            });
        }));
        private RelayCommand _appearCommand;
        /// <summary>
        /// 图片服务
        /// </summary>
        private ITodayImageService _todayImageService;
        /// <summary>
        /// 今日图片。
        /// </summary>
        public TodayImage TodayImage
        {
            get => _todayImage;
            set => Set(nameof(TodayImage), ref _todayImage, value);
        }

        /// <summary>
        /// 今日图片。
        /// </summary>
        private TodayImage _todayImage;

        /// <summary>
        /// 今日名言。
        /// </summary>
        public string TodaySentence
        {
            get => _todaySentence;
            set => Set(nameof(TodaySentence), ref _todaySentence, value);
        }

        /// <summary>
        /// 今日名言。
        /// </summary>
        private string _todaySentence;
    }
}