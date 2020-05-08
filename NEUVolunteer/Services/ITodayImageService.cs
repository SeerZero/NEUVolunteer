using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NEUVolunteer.Models;

namespace NEUVolunteer.Services
{
    /// <summary>
    /// 今日图片服务。
    /// </summary>
    public interface ITodayImageService
    {
        /// <summary>
        /// 获得今日图片。
        /// </summary>
        Task<TodayImage> GetTodayImageAsync();

        /// <summary>
        /// 检查更新。
        /// </summary>
        Task<TodayImageServiceCheckUpdateResult> CheckUpdateAsync();
    }

    /// <summary>
    /// 今日图片服务检查更新结果。
    /// </summary>
    public class TodayImageServiceCheckUpdateResult
    {
        /// <summary>
        /// 是否有更新。
        /// </summary>
        public bool HasUpdate { get; set; }

        /// <summary>
        /// 今日图片。
        /// </summary>
        public TodayImage TodayImage { get; set; }
    }
}