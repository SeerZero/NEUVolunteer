using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NEUVolunteer.Service.Implements
{
    public class NavigationService : INavigationService
    {

        private Stack<ContentPage> _pageStack = new Stack<ContentPage>();

        public NavigationService(IPageActivationService pageActivationService)
        {
            _pageActivationService = pageActivationService;
        }
        private readonly IPageActivationService _pageActivationService;

        public void NavigationTo(string pageKey, bool canBack = true)
        {
            if (canBack)
            {
                _pageStack.Push(Application.Current.MainPage as ContentPage);
                Application.Current.MainPage = _pageActivationService.Activate(pageKey);
            }
            else
            {
                if (_pageStack.Count > 0)
                {
                    _pageStack.Clear();
                }
                Application.Current.MainPage = _pageActivationService.Activate(pageKey);
            }
        }

        public void NavigationTo(string pageKey, object parameter)
        {
            _pageStack.Push(Application.Current.MainPage as ContentPage);
            var page = _pageActivationService.Activate(pageKey);
            NavigationContext.SetParameter(page, parameter);
            Application.Current.MainPage = page;
        }

        public void NavigationBack()
        {
            if (_pageStack.Count > 0)
            {
                Application.Current.MainPage = _pageStack.Pop();
            }
        }
    }
}
