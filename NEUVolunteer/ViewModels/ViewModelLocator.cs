using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight.Ioc;
using NEUVolunteer.Services.implements;
using NEUVolunteer.Services.interfaces;

namespace NEUVolunteer.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator() {
            SimpleIoc.Default.Register<CreateActivityPageViewModel>();
            SimpleIoc.Default.Register<IDBService, DBService>();
            SimpleIoc.Default.Register<IPreferenceStorage, PreferenceStorage>();
            SimpleIoc.Default.Register<CreateApplyPageViewModel>();
        }

        public CreateActivityPageViewModel CreateActivityPageViewModel =>
            SimpleIoc.Default.GetInstance<CreateActivityPageViewModel>();

        public CreateApplyPageViewModel CreateApplyPageViewModel =>
            SimpleIoc.Default.GetInstance<CreateApplyPageViewModel>();
    }
}
