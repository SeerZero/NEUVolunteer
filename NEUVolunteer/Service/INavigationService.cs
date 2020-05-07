using NEUVolunteer.View;
using System;
using System.Collections.Generic;


namespace NEUVolunteer.Service
{
    /// <summary>
    /// 导航服务接口。
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// 导航到某个页面。
        /// </summary>
        /// <param name="pageKey">被导航的页面的关键字</param>
        /// <param name="canBack">导航后是否能够返回当前页面</param>
        void NavigationTo(string pageKey, bool canBack = true);

        void NavigationTo(String pageKey, object parameter);
        /// <summary>
        /// 返回前一个页面
        /// </summary>
        void NavigationBack();
    }

    public static class NavigationServiceConstants
    {

        public static readonly string WelcomePage = nameof(WelcomePage);
        public static readonly string LoginPage = nameof(LoginPage);
        public static readonly string HomePage = nameof(HomePage);
        public static readonly string AdminPage = nameof(AdminPage);

        public static readonly Dictionary<string, Type> PageKeyTypeDictionary =
            new Dictionary<string, Type> {
                {WelcomePage, typeof(WelcomePage)},
                {LoginPage,typeof(LoginPage) },
                {AdminPage,typeof(AdminPage) },
                {HomePage,typeof(HomePage) }
            };
    }
}
