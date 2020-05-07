using GalaSoft.MvvmLight.Ioc;
using NEUVolunteer.Implementation;
using System;
using System.Collections.Generic;
using System.Text;

namespace NEUVolunteer.ViewModel
{
    public class ViewModelLocator
    {

        public ViewModelLocator()
        {
            SimpleIoc.Default.Register<MainPageViewModel>();
            SimpleIoc.Default.Register<VolunteerImplementation>();
            SimpleIoc.Default.Register<ManagerImplementation>();

        }

        public MainPageViewModel MainPageViewModel =>
            SimpleIoc.Default.GetInstance<MainPageViewModel>();
    }
}
