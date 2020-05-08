using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NEUVolunteer.Services
{
    /// <summary>
    /// 页面激活服务接口。
    /// </summary>
    public interface IPageActivationService
    {

        /// <summary>
        /// 激活需要导航的页面。
        /// </summary>
        /// <param name="pageKey">被导航的页面的关键字</param>
        /// <returns></returns>
        ContentPage Activate(string pageKey);
    }


}
