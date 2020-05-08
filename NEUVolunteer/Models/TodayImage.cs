using System;

namespace NEUVolunteer.Models
{
    /// <summary>
    /// 今日图片。
    /// </summary>
    public class TodayImage
    {
        /// <summary>
        /// 完整的开始时间。
        /// </summary>
        public string FullStartDate { get; set; }

        /// <summary>
        /// 过期时间。
        /// </summary>
        public DateTime ExpiresAt { get; set; }

        /// <summary>
        /// 版权信息。
        /// </summary>
        public string Copyright { get; set; }

        /// <summary>
        /// 版权链接。
        /// </summary>
        public string CopyrightLink { get; set; }

        /// <summary>
        /// 图片字节数组。
        /// </summary>
        public byte[] ImageBytes { get; set; }

        /// <summary>
        /// 修改范围。
        /// </summary>
        public ModificationScope Scope { get; set; }

        public enum ModificationScope
        {
            All,
            ExpiresAt
        }
    }
}
