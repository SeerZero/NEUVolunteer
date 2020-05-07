using GalaSoft.MvvmLight.Ioc;
using NEUVolunteer.Service;
using NEUVolunteer.Service.Implements;

namespace NEUVolunteer.ViewModel
{
    public class ViewModelLocator
    {
        public WelcomePageViewModel WelcomePageViewModel =>
            SimpleIoc.Default.GetInstance<WelcomePageViewModel>();
        public LoginPageViewModel LoginPageVeiwModel =>
            SimpleIoc.Default.GetInstance<LoginPageViewModel>();
        public HomePageViewModel HomePageVeiwModel =>
             SimpleIoc.Default.GetInstance<HomePageViewModel>();
        public ViewModelLocator()
        {
            SimpleIoc.Default.Register<IPageActivationService, PageActivationService>();
            SimpleIoc.Default.Register<IManager, ManagerImplementation>();
            SimpleIoc.Default.Register<IVolunteer, VolunteerImplementation>();
            SimpleIoc.Default.Register<INavigationService, NavigationService>();
            SimpleIoc.Default.Register<ITodayImageService, BingImageService>();
            SimpleIoc.Default.Register<ITodayImageStorage, TodayImageStorage>();
            SimpleIoc.Default.Register<IAlertService, AlertService>();
            SimpleIoc.Default.Register<WelcomePageViewModel>();
            SimpleIoc.Default.Register<LoginPageViewModel>();
            SimpleIoc.Default.Register<HomePageViewModel>();
            SimpleIoc.Default.Register<IMottoService, MottoService>();
        }
    }
}