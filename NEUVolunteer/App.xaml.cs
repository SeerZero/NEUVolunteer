using System;
using NEUVolunteer.ViewModels;
using NEUVolunteer.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NEUVolunteer
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new ActivityDetailPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
