using System.Threading.Tasks;
using Xamarin.Forms;


namespace NEUVolunteer.Service.Implements
{
    public class AlertService : IAlertService
    {

        private Page MainPage => _mainPage ??
            (_mainPage = Application.Current.MainPage as Page);

        private Page _mainPage;

        /******** 继承方法 ********/

        /// <summary>
        /// 显示警告。
        /// </summary>
        /// <param name="title">标题。</param>
        /// <param name="message">信息。</param>
        /// <param name="button">按钮文字。</param>
        public async Task ShowAlertAsync(string title, string message,
            string button) => await MainPage.DisplayAlert(title, message, button);

        /// <summary>
        /// 双按钮
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="accept"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        public async Task<bool> ShowAlertAsync(string title, string message,
         string accept, string cancel) =>
            await MainPage.DisplayAlert(title, message, accept, cancel);
    }
}
