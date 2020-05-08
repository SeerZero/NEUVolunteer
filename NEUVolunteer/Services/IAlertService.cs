using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace NEUVolunteer.Services
{
    /// <summary>
    /// 警告服务。
    /// </summary>
    public interface IAlertService
    {
        /// <summary>
        /// 显示警告。
        /// </summary>
        /// <param name="title">标题。</param>
        /// <param name="message">信息。</param>
        /// <param name="button">按钮文字。</param>
        Task ShowAlertAsync(string title, string message, string button);

        /// <summary>
        /// 双按钮警告
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="accept"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<Boolean> ShowAlertAsync(string title, string message, string accept,
            string cancel);
    }
}
