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
            SimpleIoc.Default.Register<ActivityDetailPageViewModel>();
            SimpleIoc.Default.Register<UpdateApplyPageViewModel>();
        }

        public CreateActivityPageViewModel CreateActivityPageViewModel =>
            SimpleIoc.Default.GetInstance<CreateActivityPageViewModel>();

        public CreateApplyPageViewModel CreateApplyPageViewModel =>
            SimpleIoc.Default.GetInstance<CreateApplyPageViewModel>();

        public ActivityDetailPageViewModel ActivityDetailPageViewModel =>
            SimpleIoc.Default.GetInstance<ActivityDetailPageViewModel>();

        public UpdateApplyPageViewModel UpdateApplyPageViewModel =>
            SimpleIoc.Default.GetInstance<UpdateApplyPageViewModel>();
    }
}
