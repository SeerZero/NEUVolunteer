using NEUVolunteer.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NEUVolunteer.Service
{
    /// <summary>
    /// 今日图片存储。
    /// </summary>
    public interface ITodayImageStorage
    {
        /// <summary>
        /// 获得今日图片。
        /// </summary>
        /// <param name="includingImageStream">是否包含图片流。</param>
        Task<TodayImage> GetTodayImageAsync(bool includingImageStream);

        /// <summary>
        /// 更新今日图片。
        /// </summary>
        /// <param name="todayImage">待更新的今日图片。</param>
        Task UpdateTodayImageAsync(TodayImage todayImage);
    }
}